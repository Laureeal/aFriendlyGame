using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersListPanel : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject playerButtonPrefab;

	void Start ()
    {
        ActualizePlayersList();
	}
	
	void ActualizePlayersList ()
    {
        foreach(KeyValuePair<string, Player> player in gameManager.players)
        {
            GameObject go = Instantiate(playerButtonPrefab, transform);
            go.GetComponentInChildren<Text>().text = player.Value.playerName;
        }
    }
}
