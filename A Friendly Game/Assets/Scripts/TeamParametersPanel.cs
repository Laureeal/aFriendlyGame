using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamParametersPanel : MonoBehaviour
{
    public GameManager gameManager;

    public Text numberPlayerLabel;
    public Text numberPlayerText;
    public Text teamLevelLabel;
    public Text teamLevelText;
    public Text socialSupportLabel;
    public Text socialSupportText;
    public Text teamAmbientLabel;
    public Text teamAmbientText;

    void Start ()
    {
        ActualizeTexts();
    }

    public void ActualizeTexts()
    {
        numberPlayerText.text = gameManager.players.Count.ToString();
        teamLevelText.text = gameManager.level.ToString();
        socialSupportText.text = gameManager.socialSupport.ToString();
        teamAmbientText.text = gameManager.teamAmbient.ToString();
    }

}
