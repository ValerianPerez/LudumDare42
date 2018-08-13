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

    /// <summary>
    /// The SceneManager of the scene
    /// </summary>
    private CustomSceneManager SceneManager;

    void Start()
    {
        CurrentState = SlotState.AVAILABLE;
        SceneManager = GameObject.Find("SceneManager").GetComponent<CustomSceneManager>();
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
                SceneManager.ClickOnFreeSlot(gameObject);
                break;
            case SlotState.OCCUPIED:
                SceneManager.ClickOnOccupiedSlot(gameObject);
                break;
            case SlotState.FROZEN:
                break;
            case SlotState.BURRIED:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// The behaviour when the space is free
    /// </summary>
    public void FreeSpace()
    {
        switch (CurrentState)
        {
            case SlotState.AVAILABLE:
                break;
            case SlotState.OCCUPIED:
                SceneManager.AddPlantResource(GetComponentInChildren<ItemController>().Harvest());
                CurrentState = SlotState.AVAILABLE;
                break;
            case SlotState.FROZEN:
                break;
            case SlotState.BURRIED:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Change the current state to PLANTING
    /// </summary>
    public void Planting()
    {
        CurrentState = SlotState.OCCUPIED;
    }
}
