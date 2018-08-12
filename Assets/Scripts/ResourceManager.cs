using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public GameObject StarshipCompartmentsGO;

    /// <summary>
    /// Define if the manager is active or not
    /// </summary>
    public bool IsActive;

    /// <summary>
    /// The current amount of water
    /// </summary>
    public float WaterResource;

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

        float decrease = 1 * Time.deltaTime;
       
        float remainingTime = WaterResource / decrease * Time.deltaTime;

        WaterResource -= decrease;
        WaterSlider.value = WaterResource;

        string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
        string seconds = (remainingTime % 60).ToString("00");

        WaterTimer.text = minutes + ":" + seconds;
        
        ManagePeople();
        ManageFood();
        MaxTotalResources = (compartments.columns * compartments.rows - compartments.damaged_count) * compartments.resource_per_compartment;
        int OccupiedByHuman = (int)Mathf.Ceil(HumanResource / compartments.resource_per_compartment);
        FoodResource = Mathf.Min(FoodResource, MaxTotalResources - OccupiedByHuman * compartments.resource_per_compartment);

        Debug.Log("HumanResource: " + HumanResource);
        Debug.Log("OccupiedByHuman: " + OccupiedByHuman);
        Debug.Log("MaxTotalResources: " + MaxTotalResources);
        Debug.Log("FoodResource: " + FoodResource);

        Debug.Log(compartments);

        compartments.Humans = HumanResource;
        compartments.Food = FoodResource;
    }

    void ManagePeople()
    {
        HumanResource += (BornPerSec - DeadPerSec) * Time.deltaTime;
        DeadCount += DeadPerSec * Time.deltaTime;
    }

    void ManageFood()
    {
        FoodResource += FoodProduction - HumanResource * FoodConsumedPerPerson;
    }
}
