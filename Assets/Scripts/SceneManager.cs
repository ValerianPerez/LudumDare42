using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// The state of active item
    /// </summary>
    public enum ActiveItemState
    {
        FREE,
        PLANTING
    }

    public GraphicRaycaster m_Raycaster;

    PointerEventData m_PointerEventData;

    EventSystem m_EventSystem;

    public Transform GameCanvas;

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
    /// The manager of slots
    /// </summary>
    [SerializeField]
    private SlotManager Slots;

    /// <summary>
    /// The resource manager
    /// </summary>
    private ResourceManager rm;

    private ActiveItemState CurrentState;

    void Start()
    {
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

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
                break;
            case "Yellow":
                ActiveItem = Instantiate(PickAPlant(), GameCanvas).GetComponent<ItemController>();
                break;
            case "Red":
                ActiveItem = Instantiate(PickAPlant(), GameCanvas).GetComponent<ItemController>();
                break;
        }
        ActiveItem.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        Slots.DisplayAvailability();

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
        Debug.Log(amount);
    }

    /// <summary>
    /// Select a plant
    /// </summary>
    /// <returns>The selected plant</returns>
    private GameObject PickAPlant()
    {
        return NormalPlant;
    }
}