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
    /// The instanciable plant object
    /// </summary>
    [SerializeField]
    private GameObject GreenPlant;

    /// <summary>
    /// The instanciable plant object
    /// </summary>
    [SerializeField]
    private GameObject RedPlant;

    /// <summary>
    /// The instanciable plant object
    /// </summary>
    [SerializeField]
    private GameObject YellowPlant;

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
                ActiveItem = Instantiate(GreenPlant, GameCanvas).GetComponent<ItemController>();
                break;
            case "Yellow":
                ActiveItem = Instantiate(YellowPlant, GameCanvas).GetComponent<ItemController>();
                break;
            case "Red":
                ActiveItem = Instantiate(RedPlant, GameCanvas).GetComponent<ItemController>();
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

        ActiveItem.ReleaseAt(position);
        ActiveItem = Instantiate(ActiveItem.gameObject, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<ItemController>();
    }
}