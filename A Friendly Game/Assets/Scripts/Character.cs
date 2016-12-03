using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public GameManager gameManager;

    public bool isInteractable = false;

    public string characterKey;
    
    public List<string> dialogueKeys;
    public List<bool> needAnswer;
    public List<string> goodAnswerKeys;

    int currentDialogueKey;

    void LaunchDialogue()
    {
        if (!isInteractable)
            return;
        gameManager.currentCharacter = this;
        string temp = dialogueKeys[currentDialogueKey];
        if (needAnswer[currentDialogueKey])
        {
            gameManager.StartDialogue(temp, temp + "a", temp + "b");
        }
        else
        {
            gameManager.StartDialogue(temp);
        }
    }

    public void CheckForGoodAnswer(string answer)
    {
        if (answer == goodAnswerKeys[currentDialogueKey])
        {
            currentDialogueKey++;
            if (currentDialogueKey >= dialogueKeys.Count)
            {
                isInteractable = false;
                gameManager.EndDialogue();
            }
            LaunchDialogue();
        }
    }
}
