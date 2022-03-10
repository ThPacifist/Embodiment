using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpozyteTalkTrigger : TalkParent
{
    /*
     * Description:
     * This script is the script for the talking point triggers, it hands off the strings and talking length 
     * to a struct and then hands that off to expozyte
     */

    //Public variables
    public ExpozyteTalk ExpTalk;
    public string dialogue1;
    public string dialogue2;
    public string dialogue3;
    public string dialogue4;
    public string dialogue5;
    public int[] dialogueLength;

    //Private variables
    dialogueStruct dialogue;
    private bool play;

    //Do on start
    private void Start()
    {
        dialogue = new dialogueStruct();
        dialogue.Dialogue = new string[dialogueLength.Length];
        switch(dialogueLength.Length)
        {
            case 5:
                dialogue.Dialogue[4] = dialogue5;
                goto case 4;
            case 4:
                dialogue.Dialogue[3] = dialogue4;
                goto case 3;
            case 3:
                dialogue.Dialogue[2] = dialogue3;
                goto case 2;
            case 2:
                dialogue.Dialogue[1] = dialogue2;
                goto case 1;
            case 1:
                dialogue.Dialogue[0] = dialogue1;
                break;
            default:
                play = false;
                break;
        }
        dialogue.timeDisplayed = dialogueLength;
    }

    //Once this trigger is entered send the struct
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (play)
        {
            ExpTalk.checkpointDialogue = dialogue;
            ExpTalk.PlayDialogue();
        }
    }
}
