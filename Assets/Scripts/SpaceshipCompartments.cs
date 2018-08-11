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
	}

    void FixedUpdate()
    {
        current_resources -= resource_drain;
    }

    // Update is called once per frame
    void Update () {
        int child_count = this.transform.childCount;
        int resources = current_resources;
        int full_compartments_count = resources / resource_per_compartment;
        Debug.Log("full_compartments_count: " + full_compartments_count);
        int partial_compartment_level = resources % resource_per_compartment;
        Debug.Log("partial_compartment_level: " + partial_compartment_level);
        for (int i = 0; i < child_count - damaged_count; i++)
        {
            Transform compartmentMask = transform.GetChild(i).Find("CompartmentMask");
            if (i < full_compartments_count)
            {
                compartmentMask.localScale = new Vector3(1,1,1);
            } else
            {
                float y_scale = partial_compartment_level * 1.0f / resource_per_compartment;
                compartmentMask.localScale = new Vector3(1, y_scale, 1);
                partial_compartment_level = 0;
            }
        }
	}
}
