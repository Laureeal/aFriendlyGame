﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(XmlReader))]
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public RectTransform dialogueBox;
    public RectTransform parametersPanel;
    public RectTransform calendarPanel;
    public RectTransform toolsPanel;

    RectTransform currentPanel = null;

    public Scrollbar dialogueScrollBar;

    public PlayersListPanel playersListPanel;

    [HideInInspector]
    public Dictionary<string, Sentence> sentences;

    [HideInInspector]
    public Dictionary<string, Character> characters;

    public Text dialogueText;
    public Button optionAButton;
    public Button optionBButton;

    public Text moneyText;
    public Text[] numberPlayerText;
    public Text[] teamLevelText;
    public Text[] socialSupportText;
    public Text[] teamMoodText;

    public Character currentCharacter;

    Text optionAButtonText;
    Text optionBButtonText;

    [HideInInspector]
    public XmlReader xmlReader;

    public Dictionary<string, Player> players;

    public string jsonName;

    public int money = 100;

    public int level = 1;
    public int socialSupport = 5;
    public int teamMood = 50;

    [HideInInspector]
    public Dictionary<int, Button> days;

    public int month = 1;
    public int today = 1;
    public int hour = 6;

    public RectTransform hourHandOfClock;

    /*    public Image night;
        public float timeToFadeToNight = 1f;*/

    public Dictionary<string, string> nameOfPlayers;

	void Awake ()
    {
        if (singleton)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
        LoadJSON();
        dialogueBox.gameObject.SetActive(false);
        xmlReader = GetComponent<XmlReader>();
        optionAButtonText = optionAButton.GetComponentInChildren<Text>();
        optionBButtonText = optionBButton.GetComponentInChildren<Text>();

        money = 100;
        level = 1;
        socialSupport = 5;
        teamMood = 50;

        SetPlayersNames();

        players = new Dictionary<string, Player>();

        AddPlayer("joc");
        AddPlayer("Pl0");

        ActualizeTexts();

        days = new Dictionary<int, Button>();
        foreach (Button button in calendarPanel.GetComponentsInChildren<Button>())
        {
            int day = 1;
            int.TryParse(button.GetComponentInChildren<Text>().text, out day);
            days.Add(day, button);
        }
    }

    void SetPlayersNames ()
    {
        nameOfPlayers = new Dictionary<string, string>();
        nameOfPlayers.Add("joc", "Jocelyne");
        nameOfPlayers.Add("Pl0", "Michelle");
        nameOfPlayers.Add("Pl1", "Missy");
        nameOfPlayers.Add("Pl2", "Soky");
        nameOfPlayers.Add("Pl3", "B.B.");
        nameOfPlayers.Add("Pl4", "Meta");
        nameOfPlayers.Add("Pl5", "Chanel");
        nameOfPlayers.Add("Pl6", "Desanges");
        nameOfPlayers.Add("Pl7", "Marie");
    }

    public void AddPlayer (string playerId)
    {
        GameObject.Instantiate(playersListPanel.playerButtonPrefab, playersListPanel.transform);
        Player newPlayer = new Player(nameOfPlayers[playerId]);
        players.Add(playerId, newPlayer);
        ActualizeTexts();
        playersListPanel.ActualizePlayersList();
    }

    public void RemovePlayer (string playerId)
    {

        if (!players.ContainsKey(playerId))
        {
            return;
        }
        players.Remove(playerId);
        ActualizeTexts();
        playersListPanel.ActualizePlayersList();
    }

    private void LoadJSON()
    {
        JSONObject obj = new JSONObject(Resources.Load<TextAsset>(jsonName).text);
        List<Sentence> s = obj.GetField("sentences").list.ConvertAll(e => new Sentence(e));
        sentences = new Dictionary<string, Sentence>();
        for (int i = 0; i < s.Count; i++)
        {
            sentences.Add(s[i].id, s[i]);
        }

        List<Character> c = obj.GetField("characters").list.ConvertAll(e => new Character(e));
        characters = new Dictionary<string, Character>();
        for (int i = 0; i < c.Count; i++)
        {
            characters.Add(c[i].id, c[i]);
        }
    }


    void EmptyTexts()
    {
        dialogueText.text = "";
        optionAButtonText.text = "";
        optionBButtonText.text = "";
        optionAButton.onClick.RemoveAllListeners();
        optionBButton.onClick.RemoveAllListeners();
    }

    void StartDialogue()
    {
        if (dialogueScrollBar != null)
            dialogueScrollBar.value = 1f;
        dialogueBox.gameObject.SetActive(true);
    }

    public void StartDialogue(string dialogueKey, string optionKey, Action action )
    {
        EmptyTexts();
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(true);
        dialogueText.text = xmlReader.GetDialogue(dialogueKey);
        optionBButtonText.text = xmlReader.GetDialogue(optionKey);
        optionBButton.onClick.AddListener(()=> {action();});
        StartDialogue();
    }

    public void StartDialogue(string dialogueKey, string optionAKey, string optionBKey, Action actionA, Action actionB)
    {
        EmptyTexts();
        optionAButton.gameObject.SetActive(true);
        optionBButton.gameObject.SetActive(true);
        dialogueText.text = xmlReader.GetDialogue(dialogueKey);
        optionAButtonText.text = xmlReader.GetDialogue(optionAKey);
        optionBButtonText.text = xmlReader.GetDialogue(optionBKey);
        optionAButton.onClick.AddListener(() => { actionA(); });
        optionBButton.onClick.AddListener(() => { actionB(); });
        StartDialogue();
    }

    public void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void ChangePanel (RectTransform panel)
    {
        if (panel == null || panel == currentPanel)
        {
            currentPanel.gameObject.SetActive(false);
            currentPanel = null;
            return;
        }
        if (currentPanel != null)
        {
            currentPanel.gameObject.SetActive(false);
        }
        currentPanel = panel;
        currentPanel.gameObject.SetActive(true);
    }

    public void OnParametersButtonClick()
    {
        ChangePanel(parametersPanel);
    }

    public void OnCalendarButtonClick()
    {
        ChangePanel(calendarPanel);
    }

    public void OnToolsButtonClick()
    {
        ChangePanel(toolsPanel);
    }

    public void ActualizeTexts()
    {
        moneyText.text = money.ToString();
        foreach(Text text in numberPlayerText)
        {
            text.text = players.Count.ToString();
        }
        foreach(Text text in teamLevelText)
        {
            text.text = level.ToString();
        }
        foreach (Text text in socialSupportText)
        {
            text.text = socialSupport.ToString();
        }
        foreach (Text text in teamMoodText)
        {
            text.text = teamMood.ToString();
        }

    }

    public void OnCharacterClick(string id)
    {
        Character c;
        characters.TryGetValue(id, out c);
        c.LaunchDialogue();
    }

    public void HourChanged ()
    {
        if (hour < 18)
        {
            hour += 3;
        }
        if (hour >= 18)
        {
            GoTomorrow();
        }
        SetHourOnClock();
    }

    public void GoTomorrow ()
    {
        DisableDayButton(today);
        if (today >= 28)
        {
            today = 0;
            NewMonth();
            month++;
        }
        today++;
        hour = 6;
//        night.gameObject.SetActive(true);
//        night.canvasRenderer.SetAlpha(0.1f);
//        night.CrossFadeAlpha(1f, timeToFadeToNight, true);
//        night.CrossFadeAlpha(0.1f, timeToFadeToNight, true);
//        night.gameObject.SetActive(false);

        //StartCoroutine(GoodNight());
    }

    void SetHourOnClock ()
    {
        float zRotation = 0f;
        switch (hour)
        {
            default:
            case 6:
            case 18:
                zRotation = 180f;          
                break;
            case 9:
                zRotation = 90f;           
                break;
            case 12:
                zRotation = 0f;
                break;
            case 15:
                zRotation = -90f;
                break;
        }
        hourHandOfClock.localEulerAngles = new Vector3(0f, 0f, zRotation);
    }

    public void NewMonth()
    {
        foreach (KeyValuePair<int, Button> day in days)
        {
            day.Value.interactable = true;
        }
    }

    public void DisableDayButton(int day)
    {
        days[day].interactable = false;
    }
}
