using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Sentence {
    const string JSONid = "idsentence";
    const string JSONidChoice = "idchoice";
    const string JSONanswers = "answers";

    public string id;
    public string text{
        get
        {
            //todo:gather the right language and text from the xml
            return "";
        }
      }
    public int idChoice;
    public List<Answer> answers;

    public Sentence(JSONObject obj)
    {
        id = obj.GetField(JSONid).str;
        idChoice = Convert.ToInt32(obj.GetField(JSONidChoice).i);
        answers = obj.GetField(JSONanswers).list.ConvertAll(e => new Answer(e));
    }
    
}
