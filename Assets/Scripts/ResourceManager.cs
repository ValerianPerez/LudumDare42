using System;
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
    public float FoodInsidePerson; // how much is in those who draw the short straw
    public double DeadCount;
    public double EatenHumans;
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

        ConsumeResourcesForTime(Time.deltaTime);


        MaxTotalResources = (compartments.columns * compartments.rows - compartments.damaged_count) * compartments.resource_per_compartment;
        int OccupiedByHuman = (int)Mathf.Ceil(HumanResource / compartments.resource_per_compartment);
        FoodResource = Mathf.Min(FoodResource, MaxTotalResources - OccupiedByHuman * compartments.resource_per_compartment);

        compartments.Humans = HumanResource;
        compartments.Food = FoodResource;

        //GameOver Condition
        if (HumanResource < 1)
        {
            GameObject.Find("SceneManager").GetComponent<CustomSceneManager>().LoseTheGame();
            IsActive = false;
        }
    }

    public void ConsumeResourcesForTime(float time)
    {
        ManagePeople(time);
        ManageFood(time);
    }

    float ManageWater()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        float water_drained = 1f;
        ItemController item;
        foreach (GameObject plant in plants)
        {
            if (plant != null) { 
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

        }
        ;
        Debug.Log("Fwater_drained: " + water_drained);
        return water_drained;
    }


    void ManagePeople(float time)
    {
        HumanResource += (BornPerSec - DeadPerSec) * HumanResource * time;
        DeadCount += DeadPerSec * HumanResource * time;
        if (FoodResource < 0)
        {
            float humans_to_be_eaten = Mathf.Ceil( -FoodResource / FoodInsidePerson);
            HumanResource -= humans_to_be_eaten;
            EatenHumans += humans_to_be_eaten;
            FoodResource = 0;
        }
        DeadCount += EatenHumans;
    }

    void ManageFood(float time)
    {
        FoodResource -= HumanResource * FoodConsumedPerPerson * time;
    }

    internal void DestroyCompartments(int destroyed)
    {
        StarshipCompartmentsGO.GetComponent<SpaceshipCompartments>().damaged_count += destroyed;
    }
}
