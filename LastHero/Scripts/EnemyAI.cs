using UnityEngine;
using System;
using System.Collections;
using LastHero;

public class EnemyAI : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerScript playerScript;

    GameObject enemyObject;
    public FlyingText smalltext;
    public PushAway pushing;
    public GetDamage getDamage;
    public int speedMove = 3;
    public int seeDistance = 6;
    public bool ok;
    void Start()
    {
        playerObject = gameObject;
        enemyObject = GameObject.FindWithTag("Player");
        ok = true;
    }

    void Update()
    {
        
        playerScript.player.Refresh();
        float directionX = enemyObject.transform.position.x - transform.position.x;
        float directionY = enemyObject.transform.position.y - transform.position.y;
        if (ok)
        if ((Mathf.Abs(directionY) < seeDistance) || (Mathf.Abs(directionX) < seeDistance))
        {
            Vector3 pos1 = transform.position;
            pos1.x += Mathf.Sign(directionX) * speedMove * Time.deltaTime;
            transform.position = pos1;

            Vector3 pos2 = transform.position;
            pos2.y += Mathf.Sign(directionY) * speedMove * Time.deltaTime;
            transform.position = pos2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            ok = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ok = true;
    }
}