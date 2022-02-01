using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject currentActiveCanvas;
    public Button currentActiveButton;
    public Button[] buttons;
    public GameObject[] childCanvases;

    private void Awake()
    {
        currentActiveCanvas.SetActive(true);
        currentActiveButton.interactable = false;
    }

    //Is called by buttons to turn on a section of the settings menu
    //Each number corresponds with the corresponding button and canvas in the respective arrays
    public void SwitchToCanvas(int index)
    {
        currentActiveCanvas.SetActive(false);
        childCanvases[index].SetActive(true);
        currentActiveCanvas = childCanvases[index];

        currentActiveButton.interactable = true;
        buttons[index].interactable = false;
        currentActiveButton = buttons[index];
    }
}
