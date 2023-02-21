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

    public double nextBeat = 0;
    public double beatCount;
    public step currentStep;
    public int grounds;
    public double latency;
    public double missRange;
    public double perfectRange;

    public List<double> rhythm = new List<double>();
    public List<(double, double)> holds = new List<(double, double)>();

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
                //beat.Play(0);
                nextBeat += 1;
            }







            if (Input.GetButtonDown("j") | Input.GetButtonDown("k"))
            {
                //Debug.Log((beatCount-0.175) % 1);
                if ((rhythm.Count == 0) && ((beatCount % 1 < (missRange + latency)) || (beatCount % 1 > ((1 - missRange) + latency))))
                {

                    if ((rhythm.Count == 0) && ((beatCount % 1 < (perfectRange + latency)) || (beatCount % 1 > ((1 - perfectRange) + latency))))
                    {
                        Debug.Log("perfect start");
                        beat.Play(0);
                    }
                    else
                    {

                        Debug.Log("good start");
                        beat.Play(0);
                    }
                    double[] tempRhythm = { };
                    (double, double)[] tempHold = { };
                    if (currentStep == step.Grinding)
                    {
                        tempRhythm = new double[] { 1,2,3};
                    }
                    if (currentStep == step.Pressing)
                    {
                        tempRhythm = new double[] { 2, 3};
                        tempHold = new (double, double)[] {(0, 1) };
                    }

                    foreach (double d in tempRhythm)
                    {
                        rhythm.Add(Mathf.Round((float)beatCount) + d);
                    }



                }
                else if (rhythm.Count != 0)
                {
                    if (((beatCount - latency) - rhythm[0]) > missRange)
                    {
                        Debug.Log("miss");
                        rhythm.RemoveAt(0);
                    }
                    else
                    {
                        if (Mathf.Abs((float)((beatCount - latency) - rhythm[0])) < missRange)
                        {
                            if (Mathf.Abs((float)((beatCount - latency) - rhythm[0])) < perfectRange)
                            {

                                Debug.Log("perfect: " + (Mathf.Abs((float)((beatCount - latency) - rhythm[0]))));
                                rhythm.RemoveAt(0);
                                beat.Play(0);
                            }
                            else
                            {
                                Debug.Log("good: " + (Mathf.Abs((float)((beatCount - latency) - rhythm[0]))));
                                rhythm.RemoveAt(0);
                                beat.Play(0);
                            }

                        }
                        else
                        {
                            Debug.Log("bad");
                        }
                    }

                }

            }
            else
            {
                if ((rhythm.Count > 0) && (((beatCount - latency) - rhythm[0]) > missRange))
                {
                    Debug.Log("miss");
                    rhythm.RemoveAt(0);
                }

            }


        }

    }
}
