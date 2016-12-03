using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    const string JSONid = "idperson";
    const string JSONisInteractable = "available";
    const string JSONappreciation = "appreciation";
    const string JSONnextSentenceId = "nextQuestion";

    public string id;
    public bool isInteractable = false;
    public int appreciation;
    public string nextSentenceId;
    public Sentence nextSentence
    {
        get
        {
            // todo: get the sentence
            return null;
        }
    }

    int currentDialogueKey;

    void LaunchDialogue()
    {
        
        if (!isInteractable)
            return;
        GameManager.singleton.currentCharacter = this;
        string dialogueKey = id+nextSentenceId;
        Sentence s = nextSentence;
        /*
        if (s.answers.Count == 2)
        {
            GameManager.singleton.StartDialogue(id + nextSentenceId, , s.answers[0].id,);
        }
        else
        {
            gameManager.StartDialogue(temp);
        }*/
    }

    public void CheckForGoodAnswer(string answer)
    {
        /*
        if (answer == goodAnswerKeys[currentDialogueKey])
        {
            currentDialogueKey++;
            if (currentDialogueKey >= dialogueKeys.Count)
            {
                isInteractable = false;
                gameManager.EndDialogue();
            }
            LaunchDialogue();
        }*/
    }

    public Character(JSONObject obj)
    {
        id = obj.GetField(JSONid).str;
        isInteractable = obj.GetField(JSONisInteractable).b;
        appreciation = Convert.ToInt32(obj.GetField(JSONisInteractable).i);
        nextSentenceId = obj.GetField(JSONnextSentenceId).str;
    }
}
