using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    /// <summary>
    /// The slots of seeds
    /// </summary>
    public List<GameObject> Slots;

    // Use this for initialization
    void Start()
    {
        //All available slots 
        Slots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Slot"));
    }

    /// <summary>
    /// Display if the slot is available or not
    /// </summary>
    public void DisplayAvailability()
    {
        foreach (GameObject slot in Slots)
        {
            slot.GetComponent<SlotController>().DisplayState();
        }
    }
}
