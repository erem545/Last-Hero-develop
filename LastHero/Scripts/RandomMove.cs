using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject obj;
    public int speed = 4;
    float time_counter;
    float move_time_counter;
    Vector3 prev_pos;

    void Start()
    {
        obj = (GameObject)this.gameObject;
        time_counter = 0f;
        move_time_counter = 0f;
        prev_pos = obj.transform.position;
    }
    Vector3 SetDirectionX(float xPos)
    {
        return new Vector3(xPos, transform.position.y, transform.position.z) - transform.position;
    }

    Vector3 SetDirectionY(float yPos)
    {
        return new Vector3(transform.position.x, yPos, transform.position.z) - transform.position;
    }

    void Update()
    {
        const int dist = 10;
        switch (new System.Random(DateTime.Now.Millisecond).Next(0,4))
        {
            case 0:
                { // Движение прямо
                        while ((move_time_counter < new System.Random(DateTime.Now.Millisecond).Next(1, 2)) || Distance(obj.transform.position, dist))
                        {
                            prev_pos = SetDirectionX(obj.transform.position.x);
                            //obj.transform.position += obj.transform.up * Time.deltaTime;
                            move_time_counter += Time.deltaTime * 0.5f;
                        }
                        Sleep(2);
                        move_time_counter = 0;
                    break;
                }
            case 1:
                { // Движение назад
                    while ((move_time_counter < new System.Random(DateTime.Now.Millisecond).Next(1, 2)) || Distance(obj.transform.position, dist))
                    {
                        prev_pos = SetDirectionX(obj.transform.position.x);
                        //obj.transform.position -= obj.transform.up * Time.deltaTime;
                        move_time_counter += Time.deltaTime * 0.5f;
                    }
                    Sleep(2);
                    move_time_counter = 0;
                    break;
                }
            case 2:
                { // Движение налево
                    while ((move_time_counter < new System.Random(DateTime.Now.Millisecond).Next(1, 2)) || Distance(obj.transform.position, dist))
                    {
                        prev_pos = SetDirectionY(obj.transform.position.y);
                        // obj.transform.position -= obj.transform.right * Time.deltaTime;
                        move_time_counter += Time.deltaTime * 0.5f;
                    }
                    Sleep(2);
                    move_time_counter = 0;
                    break;
                }
            case 3:
                { // Движение направо
                    while ((move_time_counter < new System.Random(DateTime.Now.Millisecond).Next(1, 2)) || Distance(obj.transform.position, dist))
                    {
                        prev_pos = SetDirectionY(obj.transform.position.y);
                        //obj.transform.position += obj.transform.right * Time.deltaTime;
                        move_time_counter += Time.deltaTime * 0.5f;
                    }
                    Sleep(2);
                    move_time_counter = 0;
                    break;
                }
        }
    }
    void Sleep(int seconds)
    {
        while(time_counter < seconds)
        {
            time_counter += Time.deltaTime;
        }
        time_counter = 0;
    }
    bool Distance(Vector3 vec, float max)
    {
        if (Math.Abs(vec.x) > Math.Abs(prev_pos.x + max))
        {
            vec.x = max;
            return false;
        }
        if (Math.Abs(vec.y) > Math.Abs(prev_pos.y + max))
        {
            vec.y = max;
            return false;
        }
        if (Math.Abs(vec.x) > Math.Abs((prev_pos.x + max)*-1))
        {
            vec.x = max;
            return false;
        }
        if (Math.Abs(vec.y) > Math.Abs((prev_pos.y + max) * -1))
        {
            vec.y = max;
            return false;
        }
        return true;
    }
}
