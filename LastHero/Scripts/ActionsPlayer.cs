using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LastHero;

public class ActionsPlayer : MonoBehaviour
{
    LastHero.Character setPlayer;
    bool showTooltip;
    public UpdateGUIFromPlayer gui;
    public PlayerScript playerScript;
    public FlyingText smalltext;
    public PushAway pushing;
    public GetDamage getDamage;
    Rigidbody2D rb;

    void Start()
    {
        setPlayer = playerScript.player;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {       
        if (Input.GetKey(KeyCode.Z))
        {
            setPlayer.ToDamage(2);
        }
        setPlayer.Refresh();
    }
}

