using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScripts : MonoBehaviour
{
    /*
     * Description:
     * This script controls (most) button presses for the pause menu.
     * Restart from checkpoint is the only exception and is controlled from PlyController
     */

    //Public variables and assets
    public GameObject UI;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject settingsButton;
    public GameObject menuButton;
    public GameObject resumeButton;
    public EventSystem eventS;
    public string titleScreen;

    //Private variables
    private bool showSettings = false;
    private bool paused = false;
    private bool canPause = true;
    float mouseTimer = 4;
    PlayerControls plyCntrl;
    Vector2 lastPos;
    Vector2 curPos;

    private void Awake()
    {
        plyCntrl = new PlayerControls();
        Cursor.visible = false;
    }

    //Do on enable and do on disable
    private void OnEnable()
    {
        plyCntrl.Enable();
        PlyController.Pause += pause;
        PlayerBrain.Pause += pause;
    }

    private void OnDisable()
    {
        plyCntrl.Disable();
        PlyController.Pause -= pause;
        PlayerBrain.Pause -= pause;
    }

    //Do at start
    private void Start()
    {
        lastPos = plyCntrl.UI.Point.ReadValue<Vector2>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        curPos = plyCntrl.UI.Point.ReadValue<Vector2>();
        if(curPos != lastPos)
        {
            Cursor.visible = true;
            mouseTimer = 0;
        }
        
        if(mouseTimer < 4)
        {
            mouseTimer += Time.deltaTime;
        }

        if(mouseTimer >= 4)
        {
            Cursor.visible = false;
        }
        lastPos = curPos;
    }

    //Switch from settings to menu or back
    public void settingsSwitch()
    {
        //Enable/disable pause menu
        pauseMenu.SetActive(showSettings);


        //Enable/disable the settings menu
        settingsMenu.SetActive(!showSettings);

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
    }

    //Activates whenever the ui is active
    private void onUIEnter()
    {
        //Make the cursor and UI visible, and turn time off
        Cursor.visible = true;
        UI.SetActive(true);
        Time.timeScale = 0;
        PlayerBrain.PB.canJump = false;
        PlayerBrain.PB.canMove = false;
        eventS.SetSelectedGameObject(settingsButton);
        eventS.SetSelectedGameObject(resumeButton);
    }

    //Goes whenever the ui is made not active
    public void onUIExit()
    {
        //Make the cursor and UI invisible, start time, and switch to the regular menu from settings
        Cursor.visible = false;
        if (showSettings)
        {
            settingsSwitch();
        }
        UI.SetActive(false);
        Time.timeScale = 1;
        PlayerBrain.PB.canJump = true;
        PlayerBrain.PB.canMove = true;
    }

    //Pause and unpause
    public void pause()
    {

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

    bool HasMouseMoved()
    {
        plyCntrl.UI.Point.ReadValue<Vector2>();

        return true;
    }
}
