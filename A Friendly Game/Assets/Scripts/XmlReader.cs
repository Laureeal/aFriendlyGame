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
    public int currentLanguage = 0;

    public List <Dictionary<string, string>> languages = new List<Dictionary<string, string>>();

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

            Dictionary<string, string> obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {
                obj.Add(value.Name, value.InnerText);
            }
            languages.Add(obj);
        }
    }

    public string GetDialogue(string key)
    {
        string temp = "";
        languages[currentLanguage].TryGetValue(key, out temp);
        return temp;
    }
}
