using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public FlyingText ft;
    public GameObject playerObject;
    SpriteRenderer sr;
    GUIStyle style;
    public bool ok;
    public string text;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        sr = playerObject.GetComponent<SpriteRenderer>();
        style = new GUIStyle();
        t = 0;
        ok = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ok)
        {
            ft.TextString = text;
            if (t < (1 * 0.3f))
            {
                sr.color = new Color(255, 0, 0, 255);
            }
            if ((t > (1 * 0.3f)) && (t <= (2 * 0.3f)))
            {
                sr.color = new Color(255, 255, 255, 255);
            }
            if ((t > (2 * 0.3f)) && (t <= 1))
            {
                sr.color = new Color(255, 0, 0, 255);
            }
            if ((t > 1) && (t <= (4 * 0.3f)))
            {
                sr.color = new Color(255, 255, 255, 255);
            }
            if ((t > (4 * 0.3f)) && (t <= (5 * 0.3f)))
            {
                sr.color = new Color(255, 0, 0, 255);
            }
            if ((t > (5 * 0.3f)) && (t <= (6 * 0.3f)))
            {
                sr.color = new Color(255, 255, 255, 255);
            }
            t += Time.deltaTime * 2;

            if (t > 2)
            {
                t = 0;
                ok = false;
                ft.TextString = "";
            } 
        }
        else
        {
            ok = false;
            t = 0;
            sr.color = new Color(255, 255, 255, 255);
        }
    }
}