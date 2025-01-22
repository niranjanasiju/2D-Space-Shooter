using UnityEngine;
using TMPro;
using System.Collections;

public class TimeCounter : MonoBehaviour
{
    TMP_Text timeUI;

    float startTime;//time when the user clicks on play
    float ellapsedTime;//the ellapsed time after the user clicks on play
    bool startCounter;//flag to start the counter

    int minutes;
    int seconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startCounter = false;

        //get the text ui component from the gameObject
        timeUI = GetComponent<TMP_Text>();
    }

    //function to start the time counter
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }
    //function to stop the time counter
    public void StopTimeCounter()
    {
        startCounter = false;
    }

    //set the time counter to zero
    public void ResetTimeCounter()
    {
        timeUI.text = string.Format("00:00");
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounter)
        {
            //compute the ellapsed time
            ellapsedTime = Time.time - startTime;

            minutes = (int)ellapsedTime / 60;
            seconds = (int)ellapsedTime % 60;

            //update the time counter ui text
            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
