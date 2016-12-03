using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Text;

public class XmlReader : MonoBehaviour
{
    public TextAsset dialogsDictionary;

    public string languageName;
    public int currentLanguage;

    public List <Dictionary <string, Dictionary<string, string>>> languages = new List<Dictionary<string, Dictionary<string, string>>>();

    public Text numberPlayerLabel;
    public Text teamLevelLabel;
    public Text socialSupportLabel;
    public Text teamAmbientLabel;

    void Awake ()
    {
        Reader();
	}

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dialogsDictionary.text);
        XmlNodeList languagesList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languagesList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;

            Dictionary < string,Dictionary<string, string>> obj = new Dictionary<string, Dictionary<string, string>>();

            foreach (XmlNode character in languageContent)
            {
                XmlNodeList dialogues = character.ChildNodes;
                Dictionary<string, string> obj2 = new Dictionary<string, string>();
                foreach (XmlNode value in dialogues)
                {
                    obj2.Add(value.Name, value.InnerText);
                }
                obj.Add(character.Name, obj2);
            }
            languages.Add(obj);
        }
    }

    public string GetDialogue(string characterKey, string key)
    {
        string temp = "";
        languages[currentLanguage][characterKey].TryGetValue(key, out temp);
        return temp;
    }
}
