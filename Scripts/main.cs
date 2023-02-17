using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public enum step
{
    Grinding,
    Pressing,
    Brewing,
    PouringMilk,
    Steaming,
    Combining
}

public class main : MonoBehaviour
{

    public AudioSource music;
    public AudioSource beat;
    public double starttimer;
    public bool playing = false;
    public int bpm;

    public int nextBeat = 0;
    public double beatCount;
    public step currentStep;
    public int grounds;


    public int[] rhythm;


    void Start()
    {
        currentStep = step.Grinding;
    }
    // Update is called once per frame
    void Update()
    {

        if (!playing)
        {
            starttimer += (double)Time.deltaTime;
            if (starttimer > 0)
            {
                music.Play(0);
                playing = true;
            }
        }
        else
        {
            double audioTime = ((double)music.timeSamples / music.clip.frequency) * 1000;

            beatCount = (((double)music.timeSamples / music.clip.frequency ) * bpm) / 60;


            if (beatCount > nextBeat)
            {
                beat.Play(0);
                nextBeat += (60 / (bpm));
            }

            if(currentStep == step.Grinding)
            {
                if (Input.GetButtonDown("j")|Input.GetButtonDown("k"))
                {
                    if(beatCount%1 < 0.2)
                    {
                        Debug.Log("good: " + beatCount % 1);
                    }
                    else
                    {
                        Debug.Log("bad: " + beatCount % 1);
                    }
                }
            }










        }
        



    }
}
