using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerScript playerScript;
    public FlyingText smalltext;
    public PushAway pushing;
    public float timeAttack;
    float dmg;
    GameObject enemyObject;
    float t;
    bool ok;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = gameObject;
        dmg = -1;
        t = 0;
        ok = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (t > timeAttack)
        {
            t = 0;
            ok = true;
        }

        if (ok)
        {
            smalltext.TextString = "";
            if ((enemyObject != null))
            {
                dmg = (float)Math.Round(playerScript.player.ToAttack(enemyObject.GetComponent<ActionsPlayer>().playerScript.player),3);
                if (dmg > 0)
                {
                    smalltext.TextString = "Удар!";
                    pushing.ok = true;
                    enemyObject.GetComponent<ActionsPlayer>().getDamage.ok = true;
                    enemyObject.GetComponent<ActionsPlayer>().getDamage.text = dmg.ToString();
                }
                else
                {
                    smalltext.TextString = "Промах!";
                }
                ok = false;
            }
        }
        else
        {
            if (t > timeAttack* 0.2f)
                smalltext.TextString = "";
            t += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            enemyObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyObject = null;
    }

}
