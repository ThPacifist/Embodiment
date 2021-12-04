using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    /*
     * Description:
     * This script handles all the button functions on the main menu
     */

     //Public variables and assets
    public string newGameScene;
    public GameObject levelButtonPrefab;
    public GameObject menuButtons;
    public GameObject settingsButtons;
    public GameObject levelButtons;
    public GameObject menuButton;
    public GameObject men2Button;
    public GameObject newGameButton;
    public TransitionController transCtrl;
    public EventSystem eventS;

    //Private variable
    private GameObject newButton;
    private Button button;
    private Text buttonText;
    private Vector2 buttonPos = new Vector2(-640, 390);
    private int sceneCount;

    //Enable on enable and disable on disabe
    private void OnEnable()
    {
        TransitionController.fadeOutAction += newGame;
    }

    private void OnDisable()
    {
        TransitionController.fadeOutAction -= newGame;
    }

    //Runs at start and generates buttons for each level
    private void Awake()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        //Generate buttons for the number of scenes in built settings
        for (int i = 1; i < sceneCount; i++)
        {
            //Create and place the new button in hierarchy
            newButton = Instantiate(levelButtonPrefab);
            newButton.name = "Level " + i;
            button = newButton.GetComponent<Button>();
            newButton.GetComponent<RectTransform>().SetParent(levelButtons.GetComponent<RectTransform>());
            //Place the button on the screen and change its text
            newButton.GetComponent<RectTransform>().localPosition = new Vector2(buttonPos.x + 500 * ((i - 1) / 5), buttonPos.y - 140 * ((i - 1) % 5));
            newButton.GetComponent<RectTransform>().GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + i;
            //Add listener to the button
            button.onClick.AddListener(levelSelect);
        }
    }

    //Level select buttons
    public void levelSelect()
    {
        newGameScene = eventS.currentSelectedGameObject.name;
        transCtrl.FadeOut();
    }

    //Shows the level selection
    public void levelSelection()
    {
        //Enable settings buttons
        levelButtons.gameObject.SetActive(true);
        //Disable menu buttons
        menuButtons.gameObject.SetActive(false);
        //Set active button
        eventS.SetSelectedGameObject(men2Button);
    }

    //Start new game
    public void newGame()
    {
        //Load first level/cutscene
        StartCoroutine(ChangeLevelIE());
    }

    //Go to settings menu
    public void settings()
    {
        //Enable settings buttons
        settingsButtons.gameObject.SetActive(true);
        //Disable menu buttons
        menuButtons.gameObject.SetActive(false);
        //Set active button
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
        menuButtons.gameObject.SetActive(true);
        //Disable settings buttons
        settingsButtons.gameObject.SetActive(false);
        //Disable manu buttons
        levelButtons.gameObject.SetActive(false);
        //Set active button
        eventS.SetSelectedGameObject(newGameButton);
    }

    //Timer for starting the game when new game is pressed
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
