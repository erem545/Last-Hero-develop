using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using LastHero;

public class MouseInfoPointer : MonoBehaviour
{
    GUIStyle style;
    public GameObject player;
    bool showTooltip;
    public Character character;
    void OnMouseEnter()
    {
        showTooltip = true;
        character = player.GetComponent<PlayerScript>().player;
    }
    void OnMouseExit()
    {
        showTooltip = false;
        character = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 20;
        switch (player.tag)
        {
            case "Enemy":
                {
                    style.normal.textColor = Color.red;
                    break;
                }
            case "Player":
                {
                    style.normal.textColor = Color.green;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (showTooltip)
        {
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - style.fontSize * 2, 150, 50),
                character.MainName + "\n" +
                Math.Round(character.Health, 2) + " / " + character.MaxHealth + "\n" +
                character.Info, style);
        }
    }
}
