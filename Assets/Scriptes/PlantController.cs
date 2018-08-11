using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{

    public GraphicRaycaster m_Raycaster;

    PointerEventData m_PointerEventData;

    EventSystem m_EventSystem;

    /// <summary>
    /// The current item in hand
    /// </summary>
    private ItemManager ActiveItem;

    /// <summary>
    /// The manager of slots
    /// </summary>
    [SerializeField]
    private SlotManager Slots;

    void Start()
    {
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (ActiveItem)
            {

            }
            else
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //Assign an object
                ActiveItem = results[0].gameObject.GetComponent<ItemManager>();

                Slots.DisplayAvailability();
            }

        }
    }
}
