using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    public GameObject pushingRb;
    public GameObject stoppingRb;
    float dist;
    float directionX;
    float directionY;
    float speed;
    public bool ok;
    // Start is called before the first frame update
    void Start()
    {
        directionX = 0;
        directionY = 0;
        dist = 2f;
        ok = false;
        speed = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (ok)
        {
            if ((Mathf.Abs(directionY) < dist) && (Mathf.Abs(directionX) < dist))
            {
                if ((pushingRb.transform.position.x < stoppingRb.transform.position.x) &&
                (pushingRb.transform.position.y < stoppingRb.transform.position.y))
                {
                    pushingRb.transform.position -= pushingRb.transform.up * Time.deltaTime * speed;
                    pushingRb.transform.position -= pushingRb.transform.right * Time.deltaTime * speed;
                }
                else if ((pushingRb.transform.position.x < stoppingRb.transform.position.x) &&
                (pushingRb.transform.position.y > stoppingRb.transform.position.y))
                {
                    pushingRb.transform.position += pushingRb.transform.up * Time.deltaTime * speed;
                    pushingRb.transform.position -= pushingRb.transform.right * Time.deltaTime * speed;
                }
                else if ((pushingRb.transform.position.x > stoppingRb.transform.position.x) &&
                (pushingRb.transform.position.y < stoppingRb.transform.position.y))
                {
                    pushingRb.transform.position -= pushingRb.transform.up * Time.deltaTime * speed;
                    pushingRb.transform.position += pushingRb.transform.right * Time.deltaTime * speed;
                }
                else if ((pushingRb.transform.position.x > stoppingRb.transform.position.x) &&
                 (pushingRb.transform.position.y > stoppingRb.transform.position.y))
                {
                    pushingRb.transform.position += pushingRb.transform.up * Time.deltaTime * speed;
                    pushingRb.transform.position += pushingRb.transform.right * Time.deltaTime * speed;
                }
                directionX = pushingRb.transform.position.x - stoppingRb.transform.position.x;
                directionY = pushingRb.transform.position.y - stoppingRb.transform.position.y;
            }
            else
            {
                ok = false;
            }
        }
        else
        {
            directionX = 0;
            directionY = 0;
        }
    }
}