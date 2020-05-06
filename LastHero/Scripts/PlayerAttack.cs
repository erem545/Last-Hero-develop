using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
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
            smalltext.TextString = "";
        }
        if (ok)
        {         
            if ((Input.GetKeyDown(KeyCode.Space)) && (enemyObject != null))
            {
                dmg = (float)Math.Round(playerScript.player.ToAttack(enemyObject.GetComponent<EnemyAI>().playerScript.player), 3);
                if (dmg > 0)
                {

                    smalltext.TextString = "Удар!";
                    pushing.ok = true;
                    enemyObject.GetComponent<EnemyAI>().getDamage.ok = true;
                    enemyObject.GetComponent<EnemyAI>().getDamage.text = dmg.ToString();
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
            t += Time.deltaTime;
            if (((t > timeAttack* 0.2f) && (t < timeAttack)) && (!Input.GetKey(KeyCode.Space)))
            {
                smalltext.TextString = "";
            }
            if ((Input.GetKey(KeyCode.Space)) && (t > timeAttack * 0.2f))
            {
                smalltext.TextString = "Перезарядка";
            }           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {                
        if (collision.transform.tag == "Enemy")
        {
            enemyObject = collision.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyObject = null;
    }
}
