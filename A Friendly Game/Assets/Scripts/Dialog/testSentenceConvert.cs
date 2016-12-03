using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testSentenceConvert : MonoBehaviour {
    public string jsonName;
    public List<Sentence> sentences;

	void Start () {
        sentences = new List<Sentence>();
        JSONObject obj = new JSONObject(Resources.Load<TextAsset>(jsonName).text);
        sentences = obj.GetField("sentences").list.ConvertAll(e => new Sentence(e));
	}
	
}
