using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject controller;
    public AudioClip FootSteps;    // Таблица звуков.
    private float StepTime = 0;        // Время шага.
    public AudioSource audioSource;
    private bool stepping = false; // Шагаем? По умолчанию нет.
    float t;
    void Start()
    {
        controller = GetComponent<GameObject>();
       // audioSource = GetComponent<AudioSource>();
        StepTime = 0.5f;              // назначаем время одного шага.
        t = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if ((Input.GetKey(KeyCode.W)) ||(Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) ||(Input.GetKey(KeyCode.D)))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = FootSteps;
                audioSource.Play();
            }
        }
        else //персонаж НЕ двигается
        {
            if (audioSource.isPlaying)  //если звук проигрывается
                audioSource.Stop();  //выключаем проигрывание звуков

        }

    }
}
