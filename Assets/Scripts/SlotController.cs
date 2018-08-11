using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    /// <summary>
    /// The different states of a slot
    /// </summary>
    public enum SlotState
    {
        AVAILABLE,
        OCCUPIED,
        FROZEN,
        BURRIED
    }

    /// <summary>
    /// The current state of the slot
    /// </summary>
    public SlotState CurrentState;

    void Start()
    {
        CurrentState = SlotState.AVAILABLE;
    }

    /// <summary>
    /// Display the current state of slot
    /// </summary>
	public void DisplayState()
    {
        transform.Find("AvailableSprite").gameObject.SetActive(false);

        switch (CurrentState)
        {
            case SlotState.AVAILABLE:
                transform.Find("AvailableSprite").gameObject.SetActive(true);
                break;
            case SlotState.OCCUPIED:
                break;
            case SlotState.FROZEN:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// The behaviour adding to the button
    /// </summary>
    public void OnClick()
    {
        switch (CurrentState)
        {
            case SlotState.AVAILABLE:
                GameObject.Find("SceneManager").GetComponent<SceneManager>().ClickOnSlot(transform.position);
                CurrentState = SlotState.OCCUPIED;
                break;
            case SlotState.OCCUPIED:
                break;
            case SlotState.FROZEN:
                break;
            default:
                break;
        }

        DisplayState();
    }
}
