﻿using System.Collections;
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
    /// The instanciable plant object
    /// </summary>
    [SerializeField]
    private GameObject Plant;

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
    public void ClickOnPlant()
    {
        if (ActiveItem)
        {
            return;
        }

        ActiveItem = Instantiate(Plant).GetComponent<ItemController>();
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
        ActiveItem = null;
    }
}