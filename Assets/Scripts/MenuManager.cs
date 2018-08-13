using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    /// The Game Over Screen
    /// </summary>
    public GameObject GameOverScreen;

    /// <summary>
    /// The standard social thanks screen
    /// </summary>
    public GameObject ThxScreen;

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

    /// <summary>
    /// Display the GameOver screen
    /// </summary>
    public void GameOver()
    {
        MenuUI.SetActive(false);
        GameOverScreen.SetActive(true);

        string bodyCount = GameObject.Find("SceneManager").GetComponent<ResourceManager>().DeadCount.ToString("0");
        GameOverScreen.GetComponentInChildren<Text>().text = "The last " + bodyCount + " persons of your civilisation are dead. \nYou are alone. And now ?";
    }

    /// <summary>
    /// Display the thanks screen
    /// </summary>
    public void Win()
    {
        MenuUI.SetActive(false);
        ThxScreen.SetActive(true);
    }

    /// <summary>
    /// Restart the current scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
