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

    public float Humans;
    public float Food;
    
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

        int human_compartments_full = (int)Mathf.Floor(Humans / resource_per_compartment);
        int food_compartments_full = (int)Mathf.Floor(Food / resource_per_compartment);
        float human_partial_level = Humans / resource_per_compartment - human_compartments_full;
        float food_partial_level = Food / resource_per_compartment - food_compartments_full;
        int human_partial_count = human_partial_level > 0 ? 1 : 0;


        for (int i = 0; i < child_count - damaged_count; i++)
        {
            Transform compartment = transform.GetChild(i);
            Transform compartmentMask = compartment.Find(mask_name);
            compartment.Find(damaged_name).gameObject.SetActive(false);
            GameObject food_sprite = compartment.Find(food_name).gameObject;
            GameObject human_sprite = compartment.Find(people_name).gameObject;
            
            if (i < human_compartments_full)
            {
                food_sprite.SetActive(false);
                human_sprite.SetActive(true);
                compartmentMask.localScale = new Vector3(1, 1, 1);
                //Debug.Log(compartment.name + " : human : y_scale_dmg: " + 1);
            }
            else if (i == human_compartments_full && human_partial_count > 0)
            {
                food_sprite.SetActive(false);
                human_sprite.SetActive(true);
                float y_scale = human_partial_level;
                compartmentMask.localScale = new Vector3(1, y_scale, 1);
            }
            else if (i >= human_compartments_full + human_partial_count &&  i < human_compartments_full + food_compartments_full + human_partial_count) // +1 to take the partial into account
            {
                food_sprite.SetActive(true);
                human_sprite.SetActive(false);
                compartmentMask.localScale = new Vector3(1, 1, 1);
                //Debug.Log(compartment.name + " : food : y_scale_dmg: " + 1);
            }
            else if (i == human_compartments_full + human_partial_count + food_compartments_full)
            {
                food_sprite.SetActive(true);
                human_sprite.SetActive(false);
                float y_scale = food_partial_level;
                compartmentMask.localScale = new Vector3(1, y_scale, 1);
            }
            else
            {
                food_sprite.SetActive(false);
                human_sprite.SetActive(false);
                float y_scale = food_partial_level;
                compartmentMask.localScale = new Vector3(1, 0, 1);
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
            //Debug.Log(compartment.name + " : y_scale_dmg: " + y_scale);
        }

        for (int i = child_count - damaged_count + partial_damaged_count; i < child_count; i++)
        {
            Transform compartment = transform.GetChild(i);
            Transform compartmentMask = compartment.Find(mask_name);
            compartment.Find(damaged_name).gameObject.SetActive(true);
            compartment.Find(food_name).gameObject.SetActive(false);
            compartment.Find(people_name).gameObject.SetActive(false);
            compartmentMask.localScale = new Vector3(1, 1, 1);
           // Debug.Log(compartment.name + " : y_scale_dmg: " + 1);
        }
    }

    public void ToggleRepairs()
    {
        repairing = !repairing;
    }
}
