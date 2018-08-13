using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    /// <summary>
    /// The button to access to options
    /// </summary>
    public GameObject MenuButton;

    /// <summary>
    /// The menu UI
    /// </summary>
    public GameObject MenuUI;

    /// <summary>
    /// The main title UI
    /// </summary>
    public GameObject MainTitle;

    /// <summary>
    /// Defines if the options are displayed or not
    /// </summary>
    private bool OptionsDisplayed;

    /// <summary>
    /// Close the application
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }

    /// <summary>
    /// Display options
    /// </summary>
    public void ToggleOptionsDisplay()
    {
        OptionsDisplayed = !OptionsDisplayed;
        MenuUI.SetActive(OptionsDisplayed);
    }

    /// <summary>
    /// Launch a party
    /// </summary>
    public void Play()
    {
        MainTitle.SetActive(false);
        MenuButton.SetActive(true);
    }
}
