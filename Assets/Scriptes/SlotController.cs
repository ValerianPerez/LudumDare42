using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour {
    /// <summary>
    /// The different states of a slot
    /// </summary>
    public enum SlotState
    {
        AVAILABLE,
        OCCUPIED
    }

    /// <summary>
    /// The current state of the slot
    /// </summary>
    public SlotState CurrentState;

    void Start()
    {
        CurrentState = SlotState.AVAILABLE;

        GetComponent<Button>().onClick.AddListener(() => GameObject.Find("SceneManager").GetComponent<PlantController>().ClickOnSlot(transform.position));
    }

    /// <summary>
    /// Display the current state of slot
    /// </summary>
	public void DisplayState()
    {
        switch (CurrentState)
        {
            case SlotState.AVAILABLE:
                transform.Find("AvailableSprite").gameObject.SetActive(true);
                break;
            case SlotState.OCCUPIED:
                break;
            default:
                break;
        }
    }
}
