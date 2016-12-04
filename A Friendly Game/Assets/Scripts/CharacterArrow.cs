using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CharacterArrow : MonoBehaviour {
    public string characterId;
    public float height;
    public float duration= 1;
    Vector2 pos;
    RectTransform trans;
    Image img;
    Character c;
    void Start () {
        trans = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        pos = trans.anchoredPosition;
        GameManager.singleton.characters.TryGetValue(characterId, out c);
    }
	
	// Update is called once per frame
	void Update () {
        trans.anchoredPosition = pos.SetY(pos.y + height * Mathf.Sin(Time.time / duration));

        img.enabled = c.isInteractable;
    }
}
