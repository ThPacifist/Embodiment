using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpozyteTalk : TalkParent
{
    /*
     * Description:
     * This function is what makes expozyte talk
     * It is attached to the point he is at rather than him to ensure the player can see the text at the correct spot
     */

    //Public variables
    public TextMeshProUGUI textBox;
    public GameObject bubble;
    public dialogueStruct checkpointDialogue = new dialogueStruct();

    //Private variables
    private string currentText;
    private int checkpoint;
    private int currentDialogue = 0;
    private int letter = 0;


    //Plays through the dialogue
    public void PlayDialogue()
    {
        //Check if there is anything else to display
        if (currentDialogue < checkpointDialogue.Dialogue.Length)
        {
            //Change text, make it visible and wait
            bubble.SetActive(true);
            textBox.text = "";
            currentText = checkpointDialogue.Dialogue[0];
            StartCoroutine("OneChar");
        }
    }

    //Timer to turn it off
    IEnumerator Wait()
    {
        //Keep it displayed
        yield return new WaitForSeconds(checkpointDialogue.timeDisplayed[currentDialogue]);
        bubble.gameObject.SetActive(false);
        //Clear the textbox
        textBox.text = "";
        letter = 0;
        //Put the next one up
        currentDialogue++;
        if (currentDialogue < checkpointDialogue.Dialogue.Length)
        {
            bubble.gameObject.SetActive(true);
            currentText = checkpointDialogue.Dialogue[currentDialogue];
            StartCoroutine("OneChar");
        }
        else
        {
            bubble.SetActive(false);
            currentText = "";
            currentDialogue = 0;
        }
    }

    //Put the text in there one character at a time
    IEnumerator OneChar()
    {
        Debug.Log(currentText);
        while (letter < currentText.Length)
        {
            //Put the next letter in and increment letter
            textBox.text += currentText[letter];
            letter++;
            //Next frame
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine("Wait");
    }
}
