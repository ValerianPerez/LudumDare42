using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

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

    /// <summary>
    /// Initialization of resource manager
    /// </summary>
    /// <param name="human">Number of human</param>
    /// <param name="water">Number of Water</param>
    public void Init(int human, float food, float water)
    {
        WaterResource = water;
        HumanResource = human;
        FoodResource = food;

        IsActive = true;
    }

    void Update()
    {
        if (!IsActive)
        {
            return;
        }

        float decrease = 1 * Time.deltaTime;
       
        float remainingTime = WaterResource / decrease * Time.deltaTime;

        WaterResource -= decrease;
        WaterSlider.value = WaterResource;

        string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
        string seconds = (remainingTime % 60).ToString("00");

        WaterTimer.text = minutes + ":" + seconds;
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
