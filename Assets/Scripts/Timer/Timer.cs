using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float StartTime;
    public float ElapsedTime;
    public bool Paused;
    public float TimerShifting = 1;
    public float Length;

    public AudioController AudioController;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        ElapsedTime = 0f;
        PauseTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            ElapsedTime = (Time.time - StartTime) * TimerShifting;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Paused)
            {
                ResumeTimer();
            }
            else
            {
                PauseTimer();
            }
        }
    }
    public void PauseTimer()
    {
        Paused = true;
        AudioController.AudioPause();
    }
    public void ResumeTimer()
    {
        Paused = false;
        StartTime = Time.time - ElapsedTime / TimerShifting;
        AudioController.AudioResume();
    }
    public void SetTimer(float SetTime)
    {
        PauseTimer();
        ElapsedTime = SetTime;
    }
    public float GetElapsedTime()
    {
        return ElapsedTime;
    }
    public void SetRatio(float SetRatio)
    {
        TimerShifting = SetRatio;
    }
}
