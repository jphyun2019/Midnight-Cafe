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
    public double audioTime = 0;
    public double gameTime;
    public double difference;
    public double maxDifference;

    public step currentStep;
    public int grounds;
    public double latency;
    public double missRange;
    public double perfectRange;



    public List<double> rhythm = new List<double>();


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

            gameTime += Time.deltaTime;
            audioTime = ((double)music.timeSamples / music.clip.frequency);
            difference = audioTime - gameTime;

            if(Mathf.Abs((float)difference) > maxDifference)
            {
                maxDifference = Mathf.Abs((float)difference);
            }



            beatCount = (audioTime * (double)bpm) / 60;


            if (beatCount > nextBeat)
            {
                //beat.Play(0);
                nextBeat += 1;
            }







            if (Input.GetButtonDown("j") | Input.GetButtonDown("k") | Input.GetButtonDown("d") | Input.GetButtonDown("f"))
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

                    if (currentStep == step.Grinding)
                    {
                        tempRhythm = new double[] { 1,2,3, 4, 5, 6, 7};
                    }
                    if (currentStep == step.Pressing)
                    {
                        tempRhythm = new double[] { 2, 3,4,6,7};

                    }
                    if (currentStep == step.Brewing)
                    {
                        tempRhythm = new double[] {0.5, 1, 1.5, 2, 2.5, 3, 3.5 };
                    }
                    if (currentStep == step.PouringMilk)
                    {
                        tempRhythm = new double[] {1.5, 3, 4, 5.5, 7, 7.5};
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
                        rhythm.Clear();
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
