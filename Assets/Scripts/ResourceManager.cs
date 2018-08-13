using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    public GameObject StarshipCompartmentsGO;

    /// <summary>
    /// Define if the manager is active or not
    /// </summary>
    public bool IsActive;

    /// <summary>
    /// The current amount of water
    /// </summary>
    public float WaterResource;

    public float MaxWater;

    /// <summary>
    /// The current amount of human
    /// </summary>
    public float HumanResource;

    /// <summary>
    /// The representation of water resources
    /// </summary>
    public Slider WaterSlider;

    /// <summary>
    /// The duration of current stock
    /// </summary>
    public Text WaterTimer;

    public float BornPerSec;
    public float DeadPerSec;
    public float FoodProduction;
    public float FoodResource;
    public float FoodConsumedPerPerson;
    public double DeadCount;
    public float MaxTotalResources;



    void Start()
    {

    }

    void Update()
    {
        if (!IsActive)
        {
            return;
        }
        SpaceshipCompartments compartments = StarshipCompartmentsGO.GetComponent<SpaceshipCompartments>();

        float decrease = ManageWater() * Time.deltaTime;

        float remainingTime = WaterResource / decrease * Time.deltaTime;

        WaterSlider.value = WaterResource/MaxWater;

        string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
        string seconds = (Mathf.RoundToInt(remainingTime) % 60).ToString("00");

        WaterTimer.text = minutes + ":" + seconds;

        ManagePeople();
        ManageFood();
        MaxTotalResources = (compartments.columns * compartments.rows - compartments.damaged_count) * compartments.resource_per_compartment;
        int OccupiedByHuman = (int)Mathf.Ceil(HumanResource / compartments.resource_per_compartment);
        FoodResource = Mathf.Min(FoodResource, MaxTotalResources - OccupiedByHuman * compartments.resource_per_compartment);

        compartments.Humans = HumanResource;
        compartments.Food = FoodResource;
    }

    float ManageWater()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        float water_drained = 1f;
        Debug.Log("Found plants: " + plants.Length);
        ItemController item;
        foreach (GameObject plant in plants)
        {
            item = plant.GetComponent<ItemController>();
            
            if (WaterResource - item.GetWaterDrain() <= 0)
            {
                WaterResource = 0;
                item.SoDryIWantToDie();
            } else
            {
                WaterResource -= item.GetWaterDrain();
                water_drained += item.GetWaterDrain();
            }
            
        }
        ;
        Debug.Log("Fwater_drained: " + water_drained);
        return water_drained;
    }


    void ManagePeople()
    {
        HumanResource += (BornPerSec - DeadPerSec) * HumanResource * Time.deltaTime;
        DeadCount += DeadPerSec * Time.deltaTime;
    }

    void ManageFood()
    {
        FoodResource -= HumanResource * FoodConsumedPerPerson * Time.deltaTime;
    }
}
