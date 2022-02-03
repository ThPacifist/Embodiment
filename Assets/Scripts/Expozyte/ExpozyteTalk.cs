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
    public TextMeshPro textBox;
    public string[] Dialogue;
    public int[] timeDisplayed;

    //Private variables
    private int currentDialogue = 0;

    //Detects when expozyte enters the checkpoint
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Caught you");
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
        yield return new WaitForSeconds(timeDisplayed[currentDialogue]);
        textBox.gameObject.SetActive(false);
        //Turn it off for a little bit
        yield return new WaitForSeconds(0.5f);
        //Put the next one up
        currentDialogue++;
        PlayDialogue();
    }
}
