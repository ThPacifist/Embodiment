using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpozyteTalk : MonoBehaviour
{
    /*
     * Description:
     * This function is what makes expozyte talk
     * It is attached to the point he is at rather than him to ensure the player can see the text at the correct spot
     */

    //Public variables
    public TextMeshProUGUI textBox;
    public string[] Dialogue;

    //Private variables
    private int currentDialogue;

    //Detects when expozyte enters the checkpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayDialogue();
    }

    //Plays through the dialogue
    private void PlayDialogue()
    {
        //Check if there is anything else to display
        if (currentDialogue < Dialogue.Length)
        {
            //Change text, make it visible and wait
            textBox.text = Dialogue[currentDialogue];
            textBox.gameObject.SetActive(true);
            StartCoroutine("Wait");
        }
    }

    //Timer to turn it off
    IEnumerator Wait()
    {
        //Keep it displayed
        yield return new WaitForEndOfFrame();
        textBox.gameObject.SetActive(false);
        //Turn it off for a little bit
        yield return new WaitForEndOfFrame();
        //Put the next one up
        currentDialogue++;
        PlayDialogue();
    }
}
