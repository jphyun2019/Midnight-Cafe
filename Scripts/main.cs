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
    public float starttimer = -2000;
    public bool playing = false;
    public int bpm;

    public int nextBeat = 0;
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
            starttimer += Time.deltaTime * 1000;
            if (starttimer > 0)
            {
                music.Play(0);
                playing = true;
            }
        }
        else
        {
            double audioTime = ((double)music.timeSamples / music.clip.frequency) * 1000;

            Debug.Log(audioTime);

            if (audioTime > nextBeat)
            {
                beat.Play(0);
                nextBeat += (60000 / (bpm*4));
            }

            if(currentStep == step.Grinding)
            {
                
            }










        }
        



    }
}
