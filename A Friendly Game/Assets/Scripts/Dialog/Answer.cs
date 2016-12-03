using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class Answer {
    const string JSONid = "idsentence";
    const string JSONresults = "result";
    const string JSONmoney = "money";
    const string JSONteamAtmosphere = "teamatmosphere";
    const string JSONsocialAppreciation = "socialappreciation";
    const string JSONappreciation = "appreciation";
    const string JSONaddPlayerIds = "playeradd";
    const string JSONlosePlayerIds = "playerlose";

    public string id;
    public string text
    public bool hasResponse { get
        {
            return id != "0";
        }
    }
    public int money;
    public int teamAtmosphere;
    public int socialAppreciation;
    public int appreciation;
    public List<string> addPlayerIds;
    public List<string> losePlayerIds;
    public List<Result> results;

    protected void ApplyStats()
    {
        //todo:apply them
    }
    public void Choose()
    {
        ApplyStats();
        //todo:add player
        //todo:remove player
        for (int i = 0; i < results.Count; i++)
        {
            results[i].Apply();
        }
    }

    public Answer(JSONObject obj)
    {
        id = obj.GetField(JSONid).str;
        money = Convert.ToInt32(obj.GetField(JSONmoney).i);
        teamAtmosphere = Convert.ToInt32(obj.GetField(JSONteamAtmosphere).i);
        socialAppreciation = Convert.ToInt32(obj.GetField(JSONsocialAppreciation).i);
        appreciation = Convert.ToInt32(obj.GetField(JSONappreciation).i);
        addPlayerIds = obj.GetField(JSONaddPlayerIds).list.ConvertAll(e => e.str);
        losePlayerIds = obj.GetField(JSONlosePlayerIds).list.ConvertAll(e => e.str);
        results = obj.GetField(JSONresults).list.ConvertAll(e => new Result(e));

    }

    [Serializable]
    public class Result
    {
        const string JSONcharacterId = "idperson";
        const string JSONsetInteractable = "available";
        const string JSONnextSentence = "idsentence";
        const string JSONcontinueRightNow = "rightnow";

        public string characterId;
        public bool setInteractable;
        public string nextSentence;
        public bool continueRightNow;

        public void Apply() {
            Character c;
            GameManager.singleton.characters.TryGetValue(characterId, out c);
            c.isInteractable = setInteractable;
            c.nextSentenceId = nextSentence;
            //todo:hande continue right now

        }

        public Result (JSONObject obj)
        {
            characterId = obj.GetField(JSONcharacterId).str;
            setInteractable = obj.GetField(JSONsetInteractable).b;
            nextSentence = obj.GetField(JSONnextSentence).str;
            continueRightNow = obj.GetField(JSONsetInteractable).b;
        }
    }
}
