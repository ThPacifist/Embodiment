using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScripts : MonoBehaviour
{
    //Script created on 9/20 by Jason
    //Made the pause mnu open and close with the button on 9/22 by Jason
    /*
     * TODO:
     */

    //Public variables and assets
    public GameObject UI;
    public GameObject[] pauseMenu;
    public GameObject[] settingsMenu;
    public GameObject settingsButton;
    public GameObject menuButton;
    public EventSystem eventS;
    public string titleScreen;

    //Private variables
    private bool showSettings = false;
    private bool paused = false;
    private bool canPause = true;

    //Do on enable and do on disable
    private void OnEnable()
    {
        PlyController.Pause += pause;
    }

    private void OnDisable()
    {
        PlyController.Pause -= pause;
    }

    //Do at start
    private void Start()
    {
        //Cursor.visible = false;
    }

    //Switch from settings to menu or back
    public void settingsSwitch()
    {
        //Enable/disable pause menu
        for(int i = 0; i < pauseMenu.Length; i++)
        {
            pauseMenu[i].SetActive(showSettings);
        }

        //Enable/disable the settings menu
        for (int i = 0; i < settingsMenu.Length; i++)
        {
            settingsMenu[i].SetActive(!showSettings);
        }

        //Invert the boolean
        if (showSettings)
        {
            eventS.SetSelectedGameObject(settingsButton);
            showSettings = false;
        }
        else
        {
            showSettings = true;
            eventS.SetSelectedGameObject(menuButton);
        }

        //Enable/disable the settings menu
        for(int i = 0; i < settingsMenu.Length; i++)
        {
            settingsMenu[i].SetActive(showSettings);
        }
    }

    //Activates whenever the ui is active
    private void onUIEnter()
    {
        Debug.Log("Paused");
        //Make the cursor and UI visible, and turn time off
        Cursor.visible = true;
        UI.SetActive(true);
        Time.timeScale = 0;
    }

    //Goes whenever the ui is made not active
    public void onUIExit()
    {
        Debug.Log("Unpause");
        //Make the cursor and UI invisible, start time, and switch to the regular menu from settings
        Cursor.visible = false;
        if (showSettings)
        {
            settingsSwitch();
        }
        UI.SetActive(false);
        Time.timeScale = 1;
    }

    //Pause and unpause
    public void pause()
    {
        //Wait before pausing again
        if (canPause)
        {
            //Wait before next pause
            canPause = false;
            StartCoroutine(waitAFrame());
            //If it is paused, unpause
            if (paused)
            {
                onUIExit();
                paused = false;
            }
            //If it isn't paused, pause
            else
            {
                onUIEnter();
                paused = true;
            }
        }
    }

    //Quit to title screen
    public void toTitleScreen()
    {
        //Save game
        Time.timeScale = 1;
        SceneManager.LoadScene(titleScreen);
    }

    //Quit game
    public void quitGame()
    {
        //Save game
        Application.Quit();
    }

    //Restart Level
    public void restartLevel()
    {
        //Reload the scene
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    //Don't pause for the rest of this frame
    IEnumerator waitAFrame()
    {
        yield return new WaitForEndOfFrame();
        canPause = true;
    }

}
