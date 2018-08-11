using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCompartments : MonoBehaviour {

    public Transform compartment_prefab;
    public int columns;
    public int rows;
    public float x_start;
    public float x_end;
    public float y_start;
    public float y_end;
    public int resource_per_compartment;
    public int current_resources;
    public int resource_drain;
    public int damaged_count;
    public int repair_speed;
    public bool repairing;
    public int repair_missing;
    public int compartment_max_hp;
    int partial_damaged_count;

    const string mask_name = "CompartmentMask";
    const string food_name = "CompartmentFood";
    const string people_name = "CompartmentPeople";
    const string damaged_name = "CompartmentDamaged";


    public ArrayList[] compartments;

	// Use this for initialization
	void Start () {
        float offset_x = (x_end - x_start) / columns;
        float offset_y = (y_end - y_start) / rows;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Transform newCompartment = Instantiate(compartment_prefab, new Vector3(this.transform.position.x + offset_x/2 + x_start + j * offset_x, this.transform.position.y + offset_y / 2 + y_start + i * offset_y), Quaternion.identity, this.transform);
                newCompartment.name = "compartment_" + i + "_" + j;
            }
        }
        repair_missing = damaged_count * compartment_max_hp;

    }

    void FixedUpdate()
    {
        current_resources -= resource_drain;
        if (repairing)
        {
            repair_missing -= repair_speed;
            partial_damaged_count = repair_missing % compartment_max_hp > 0 ? 1 : 0;
            damaged_count = (repair_missing / compartment_max_hp) + partial_damaged_count;
            if (repair_missing <= 0)
            {
                damaged_count = 0;
                repairing = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update () {
        int child_count = this.transform.childCount;
        int resources = current_resources;
        int full_compartments_count = resources / resource_per_compartment;
        int partial_compartment_level = resources % resource_per_compartment;
        for (int i = 0; i < child_count - damaged_count; i++)
        {
            Transform compartment = transform.GetChild(i);
            Transform compartmentMask = compartment.Find(mask_name);
            compartment.Find(damaged_name).gameObject.SetActive(false);
            compartment.Find(food_name).gameObject.SetActive(false);
            compartment.Find(people_name).gameObject.SetActive(true);
            if (i < full_compartments_count)
            {
                compartmentMask.localScale = new Vector3(1,1,1);
                Debug.Log(compartment.name + " : y_scale_dmg: " + 1);
            } else
            {
                float y_scale = partial_compartment_level * 1.0f / resource_per_compartment;
                compartmentMask.localScale = new Vector3(1, y_scale, 1);
                Debug.Log(compartment.name + " : y_scale_dmg: " + y_scale);
                partial_compartment_level = 0;
            }
        }

        for (int i = child_count - damaged_count; i < child_count - damaged_count + partial_damaged_count; i++)
        {
            Transform compartment = transform.GetChild(i);
            Transform compartmentMask = compartment.Find(mask_name);
            compartment.Find(damaged_name).gameObject.SetActive(true);
            compartment.Find(food_name).gameObject.SetActive(false);
            compartment.Find(people_name).gameObject.SetActive(false);
            float y_scale = (repair_missing % compartment_max_hp) * 1.0f / compartment_max_hp;
            compartmentMask.localScale = new Vector3(1, y_scale, 1);
            Debug.Log(compartment.name + " : y_scale_dmg: " + y_scale);
        }

        for (int i = child_count - damaged_count + partial_damaged_count; i < child_count; i++)
        {
            Transform compartment = transform.GetChild(i);
            Transform compartmentMask = compartment.Find(mask_name);
            compartment.Find(damaged_name).gameObject.SetActive(true);
            compartment.Find(food_name).gameObject.SetActive(false);
            compartment.Find(people_name).gameObject.SetActive(false);
            compartmentMask.localScale = new Vector3(1, 1, 1);
            Debug.Log(compartment.name + " : y_scale_dmg: " + 1);
        }
    }

    public void ToggleRepairs()
    {
        repairing = !repairing;
    }
}
