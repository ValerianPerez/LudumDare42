using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

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

    void Start()
    {
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        rm = GetComponent<ResourceManager>();
        rm.Init(1000, 1000, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(ActiveItem.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClickOnPlant(string color)
    {
        if (ActiveItem)
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
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    public void ClickOnSlot(Vector2 position)
    {
        if (!ActiveItem)
        {
            return;
        }

        Instantiate(ActiveItem.gameObject, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<ItemController>().ReleaseAt(position);
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