using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(XmlReader))]
public class GameManager : MonoBehaviour
{
    public RectTransform dialogueBox;

    public Text dialogueText;
    public Button optionAButton;
    public Button optionBButton;

    [HideInInspector]
    public Character currentCharacter;

    Text optionAButtonText;
    Text optionBButtonText;

    [HideInInspector]
    public XmlReader xmlReader;

    public List<Player> players;

    public int level = 1;
    public int socialSupport = 0;
    public int teamAmbient = 0;

	void Start ()
    {
        dialogueBox.gameObject.SetActive(false);
        xmlReader = GetComponent<XmlReader>();
        optionAButtonText = optionAButton.GetComponentInChildren<Text>();
        optionBButtonText = optionBButton.GetComponentInChildren<Text>();
    }

    void EmptyTexts()
    {
        dialogueText.text = "";
        optionAButtonText.text = "";
        optionBButtonText.text = "";
    }

    void StartDialogue()
    {
        dialogueBox.gameObject.SetActive(true);
    }

    public void StartDialogue(string dialogueKey)
    {
        EmptyTexts();
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(true);
        dialogueText.text = xmlReader.GetDialogue(currentCharacter.characterKey, dialogueKey);
        optionBButtonText.text = "OK";
        StartDialogue();
    }

    public void StartDialogue(string dialogueKey, string optionAKey, string optionBKey)
    {
        EmptyTexts();
        optionAButton.gameObject.SetActive(true);
        optionBButton.gameObject.SetActive(true);
        dialogueText.text = xmlReader.GetDialogue(currentCharacter.characterKey, dialogueKey);
        optionAButtonText.text = xmlReader.GetDialogue(currentCharacter.characterKey, optionAKey);
        optionBButtonText.text = xmlReader.GetDialogue(currentCharacter.characterKey, optionBKey);
        StartDialogue();
    }

    public void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void OnButtonClick(string button)
    {
        currentCharacter.CheckForGoodAnswer(button);
    }
}
