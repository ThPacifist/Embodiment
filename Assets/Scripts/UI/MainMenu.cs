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
     */

     //Public variables and assets
    public string newGameScene;
    public Transform[] menuButtons;
    public Transform[] settingsButtons;
    public GameObject settingsButton;
    public GameObject menuButton;
    public EventSystem eventS;

    //Private variable

    private void OnEnable()
    {
        TransitionController.fadeOutAction += newGame;
    }

    private void OnDisable()
    {
        TransitionController.fadeOutAction -= newGame;
    }


    //Start new game
    public void newGame()
    {
        //Load first level/cutscene
        ChangeLevel();
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

    public void ChangeLevel()
    {
        StartCoroutine(ChangeLevelIE());
    }

    IEnumerator ChangeLevelIE()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newGameScene);//Loads next scene in background asyncronously

        while (!asyncLoad.isDone)// Run this code until the next scene is done loading
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene); //Unloads current scene
    }
}
