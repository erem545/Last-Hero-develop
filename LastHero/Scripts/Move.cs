using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LastHero;
using UnityEditor;
public class Move : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerScript playerScript;

    public Camera cam;
    Vector3 camPos;

    public static int speed = 3;
    int saveSpeed;
    public float direction;

    Animator anim;
    void Start()
    {
        playerObject = gameObject;
        anim = gameObject.GetComponent<Animator>();
        saveSpeed = speed;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            direction = -1;
            Shift(3);
            playerObject.transform.position += playerObject.transform.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = 0;
            Shift(3);
            playerObject.transform.position -= playerObject.transform.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -2;
            Shift(3);
            playerObject.transform.position -= playerObject.transform.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = -3;
            Shift(3);
            playerObject.transform.position += playerObject.transform.right * speed * Time.deltaTime;
        }
        anim.SetFloat("Direction", direction);
        speed = saveSpeed;

        camPos.x = playerObject.transform.position.x;
        camPos.y = playerObject.transform.position.y;
        camPos.z = playerObject.transform.position.z - 20;
        cam.transform.position = camPos;
    }

    void Shift(int _speed)
    {
        // Ускорение
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (playerScript.player.Endurance > 1)
            {
                speed = saveSpeed + _speed;
                playerScript.player.Endurance -= 0.1f;               
            }
        }
    }
}
