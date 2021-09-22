using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    //Script created on 9/20 by Jason
    /*
     * TODO:
     * Make controller stuff work
     * Make UI appear when escape is hit
     */

    //Public variables and assets
    public GameObject UI;
    public GameObject[] pauseMenu;
    public GameObject[] settingsMenu;

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

    //Calls every update
    private void Update()
    {
        
    }

    //Do at start
    private void Start()
    {
        Cursor.visible = true;
    }

    //Switch from settings to menu or back
    private void settingsSwitch()
    {
        //Enable/disable pause menu
        for(int i = 0; i < pauseMenu.Length; i++)
        {
            pauseMenu[i].SetActive(showSettings);
        }

        //Invert the boolean
        if(showSettings)
        {
            showSettings = false;
        }
        else
        {
            showSettings = true;
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
        Cursor.visible = true;
        UI.SetActive(true);
        Time.timeScale = 0;
    }

    //Goes whenever the ui is not active
    private void onUIExit()
    {
        Cursor.visible = false;
        UI.SetActive(false);
        Time.timeScale = 1;
    }

    //Pause and unpause
    private void pause()
    {
        if (canPause)
        {
            if (paused)
            {
                onUIExit();
                paused = false;
            }
            else
            {
                onUIEnter();
                paused = true;
            }
            //canPause = false;
            StartCoroutine(PauseTimer());
        }
    }

    //Wait between pauses
    IEnumerator PauseTimer()
    {
        yield return new WaitForSeconds(0.1f);
        canPause = true;
    }

}
