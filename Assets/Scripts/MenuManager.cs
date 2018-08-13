using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

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
    /// The winner screen
    /// </summary>
    public GameObject WinnerScreen;

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

        ResourceManager rm = GameObject.Find("SceneManager").GetComponent<ResourceManager>();

        string bodyCount = rm.DeadCount.ToString("0");
        string eatenHumans = rm.EatenHumans.ToString("0");
        GameOverScreen.GetComponentInChildren<Text>().text = "The last " + bodyCount + " persons of your civilization are dead and " + eatenHumans + " were eaten. \nYou are alone. And now ?";
    }

    /// <summary>
    /// Display the resume screen
    /// </summary>
    public void Win()
    {
        MenuUI.SetActive(false);
        WinnerScreen.SetActive(true);

        ResourceManager rm = GameObject.Find("SceneManager").GetComponent<ResourceManager>();

        string savedLifes = rm.HumanResource.ToString("0");

        WinnerScreen.GetComponentInChildren<Text>().text = "Congratulations !\nYou saved the last " + savedLifes + " persons of your civilization !";

        if (rm.EatenHumans != 0)
        {
            WinnerScreen.GetComponentInChildren<Text>().text += "\nUnfortunately, people have eaten " + rm.EatenHumans.ToString("0") + " other persons to survive...";
        }
    }

    /// <summary>
    /// Display the thanks screen
    /// </summary>
    public void ContinueWin()
    {
        WinnerScreen.SetActive(false);
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
