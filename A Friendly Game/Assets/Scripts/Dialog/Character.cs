using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    const string JSONid = "idperson";
    const string JSONisInteractable = "available";
    const string JSONappreciation = "appreciation";
    const string JSONnextSentenceId = "nextquestion";

    public string id;
    public bool isInteractable = false;
    public int appreciation;
    public string nextSentenceId;
    public Sentence nextSentence
    {
        get
        {
            Sentence s;
            GameManager.singleton.sentences.TryGetValue(nextSentenceId, out s);
            return s;
        }
    }

    int currentDialogueKey;

    public void LaunchDialogue()
    {

        if (!isInteractable)
            return;
        GameManager.singleton.currentCharacter = this;
        string dialogueKey = id + nextSentenceId;
        Sentence s = nextSentence;
        switch (s.idChoice)
        {
            case 0:
                GameManager.singleton.StartDialogue(id + nextSentenceId, "ok", s.answers[0].Choose);
                break;
            case 1:
                GameManager.singleton.StartDialogue(id + nextSentenceId, "yes", "no", s.answers[0].Choose, s.answers[1].Choose);
                break;
            case 2:
                GameManager.singleton.StartDialogue(id + nextSentenceId, "yes", "maybe", s.answers[0].Choose, s.answers[1].Choose);
                break;
            default:
                break;
        }
    }

    public Character(JSONObject obj)
    {
        id = obj.GetField(JSONid).str;
        isInteractable = obj.GetField(JSONisInteractable).b;
        appreciation = Convert.ToInt32(obj.GetField(JSONisInteractable).i);
        nextSentenceId = obj.GetField(JSONnextSentenceId).str;
    }
}
