using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer {


    public string id;
    public string text
    {
        get
        {
            //gather the right language and text from the xml
            return "";
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
        //apply money
    }
    public void Choose()
    {
        ApplyStats();
        //add player
        //remove player
        for (int i = 0; i < results.Count; i++)
        {
            results[i].Apply();
        }
    }

    public class Result
    {
        public string CharacterId;
        public bool setInteractable;
        public string nextSentence;
        public bool continueRightNow;

        public void Apply() {
            //take character
            //set stuff

        }
    }
}
