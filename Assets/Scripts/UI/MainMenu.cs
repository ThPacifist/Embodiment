using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Created by Jason on 9/14
    /*
     * TODO:
     * Add direct functionality to these buttons
     */

     //Public variables and assets
    public string newGameSecne;
    public Transform[] menuButtons;
    public Transform[] settingsButtons;
    public GameObject settingsButton;
    public GameObject menuButton;
    public EventSystem eventS;

    //Private variable


    //Start new game
    public void newGame()
    {
        //Load first level/cutscene
        SceneManager.LoadScene(newGameSecne);
    }

    //Continue game
    public void continueGame()
    {
        //Load saved game from however that works
    }

    //Go to settings menu
    public void settings()
    {
        //Enable settings buttons
        for (int i = 0; i < settingsButtons.Length; i++)
        {
            settingsButtons[i].gameObject.SetActive(true);
        }
        //Disable menu buttons
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
        eventS.SetSelectedGameObject(menuButton);
    }

    //Quit Game
    public void quitGame()
    {
        Application.Quit();
    }

    //Back to menu
    public void backToMenu()
    {
        //Enable menu buttons
        for(int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(true);
        }
        //Disable settings buttons
        for(int i = 0; i < settingsButtons.Length; i++)
        {
            settingsButtons[i].gameObject.SetActive(false);
        }
        eventS.SetSelectedGameObject(settingsButton);
    }
}
