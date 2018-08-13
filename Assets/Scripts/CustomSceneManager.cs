using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviour
{
    /// <summary>
    /// The state of active item
    /// </summary>
    public enum ActiveItemState
    {
        FREE,
        PLANTING
    }
    
    public Transform GameCanvas;

    /// <summary>
    /// The Navigation UI
    /// </summary>
    public GameObject NavigationUI;

    /// <summary>
    /// The UI to display when landing
    /// </summary>
    public GameObject LandingUI;

    /// <summary>
    /// The UI to display when option button is pressed
    /// </summary>
    public GameObject MenuUI;

    /// <summary>
    /// Different plants
    /// </summary>
    public GameObject FrozenDeadPlant;
    public GameObject FrozenPlant;
    public GameObject NormalPlant;
    public GameObject RadioactivePlant;
    public GameObject SickPlant;

    /// <summary>
    /// The current item in hand
    /// </summary>
    private ItemController ActiveItem;


    /// <summary>
    /// The resource manager
    /// </summary>
    private ResourceManager rm;

    /// <summary>
    /// The state of cursor
    /// </summary>
    private ActiveItemState CurrentState;

    /// <summary>
    /// The current displaying UI
    /// </summary>
    private GameObject CurrentUI;

    public float BasicWaterLevel;

    public float DefaultTravelTime;

    

    void Start()
    {
        rm = GetComponent<ResourceManager>();
        CurrentState = ActiveItemState.FREE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !CurrentState.Equals(ActiveItemState.FREE))
        {
            Destroy(ActiveItem.gameObject);
            CurrentState = ActiveItemState.FREE;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClickOnPlant(string color)
    {
        if (CurrentState.Equals(ActiveItemState.PLANTING))
        {
            Destroy(ActiveItem.gameObject);
        }

        switch (color)
        {
            case "Green":
                ActiveItem = Instantiate(PickAPlant(), GameCanvas).GetComponent<ItemController>();
                ActiveItem.HarvestAmount *= 1;
                ActiveItem.WaterDrain *= 1;
                break;
            case "Yellow":
                ActiveItem = Instantiate(PickAPlant(), GameCanvas).GetComponent<ItemController>();
                ActiveItem.HarvestAmount *= 1.25f;
                ActiveItem.WaterDrain *= 1.5f;
                break;
            case "Red":
                ActiveItem = Instantiate(PickAPlant(), GameCanvas).GetComponent<ItemController>();
                ActiveItem.HarvestAmount *= 1.4f;
                ActiveItem.WaterDrain *= 2.5f;
                break;
        }
        ActiveItem.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        CurrentState = ActiveItemState.PLANTING;
    }

    /// <summary>
    /// Function call when palyer clicks on free slot
    /// </summary>
    /// <param name="gameObject">The gameobject which contain the clicked button</param>
    public void ClickOnFreeSlot(GameObject gameObject)
    {
        if (CurrentState.Equals(ActiveItemState.FREE))
        {
            return;
        }

        GameObject go = Instantiate(ActiveItem.gameObject, GameObject.FindGameObjectWithTag("Canvas").transform);
        ActiveItem.ReleaseAt(gameObject);
        ActiveItem = go.GetComponent<ItemController>();

        gameObject.GetComponent<SlotController>().Planting();
    }

    /// <summary>
    /// Function call when palyer clicks on occupied slot
    /// </summary>
    /// <param name="gameObject">The gameobject which contain the clicked button</param>
    public void ClickOnOccupiedSlot(GameObject gameObject)
    {
        switch (CurrentState)
        {
            case ActiveItemState.FREE:
                gameObject.GetComponent<SlotController>().FreeSpace();
                break;
            case ActiveItemState.PLANTING:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Add the amount of plant resources
    /// </summary>
    /// <param name="amount">The amount to add</param>
    public void AddPlantResource(float amount)
    {
        rm.FoodResource += amount;
    }

    /// <summary>
    /// Select a plant
    /// </summary>
    /// <returns>The selected plant</returns>
    private GameObject PickAPlant()
    {
        return NormalPlant;
    }

    /// <summary>
    /// Call on travel to the selected planet
    /// </summary>
    /// <param name="planet">The selected planet</param>
    public void TravelTo(Planet planet)
    {
        LandingUI.SetActive(true);
        rm.MaxWater = planet.GetWaterMultiplier() * BasicWaterLevel;
        rm.WaterResource = rm.MaxWater;
        rm.ConsumeResourcesForTime(DefaultTravelTime);
        rm.IsActive = true;
        NavigationUI.SetActive(false);
        rm.DestroyCompartments(Mathf.RoundToInt(planet.GetRadiation()));
    }

    /// <summary>
    /// Return to the navigation UI after a planet
    /// </summary>
    public void LaunchFromPlanet()
    {
        rm.IsActive = false;
        LandingUI.SetActive(false);
        NavigationUI.SetActive(true);
    }

    /// <summary>
    /// Win the game and display final screen
    /// </summary>
    public void WinTheGame()
    {
        LandingUI.SetActive(false);
        MenuUI.GetComponent<MenuManager>().Win();
    }

    /// <summary>
    /// Lose the game and display final screen
    /// </summary>
    public void LoseTheGame()
    {
        LandingUI.SetActive(false);
        Debug.Log("---------------------- GAME OVER -------------------");
        MenuUI.GetComponent<MenuManager>().GameOver();
    }
}