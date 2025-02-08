using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioSource;
    public Timer Timer;
    public ChartLoader ChartLoader;

    IEnumerator Start()
    {
        string ManagerSetChartPath = System.Environment.CurrentDirectory + "/LoadPath.txt";
        string AudioPath = File.ReadAllLines(ManagerSetChartPath)[1].Trim();
        AudioType AudioType = DetectAudioType(AudioPath);
        using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + AudioPath, AudioType))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
        Timer.Length = AudioSource.clip.length;
        AudioResume();
        AudioPause();
    }
    public void AudioPause()
    {
        AudioSource.Pause();
    }
    public void AudioResume()
    {
        float elapsedTime = Timer.GetElapsedTime() - ChartLoader.GetChart().offset / 1000f;
        if (elapsedTime >= 0)
        {
            AudioSource.time = elapsedTime;
        }
        else
        {
            AudioSource.time = 0;
        }
        AudioSource.pitch = Timer.TimerShifting;
        AudioSource.Play();
    }
    public AudioType DetectAudioType(string Path) 
    {
        string extension = System.IO.Path.GetExtension(Path).ToLower();
        switch (extension) 
        {
            case ".mp3": return AudioType.MPEG;
            case ".ogg": return AudioType.OGGVORBIS;
            case ".wav": return AudioType.WAV;
            default: return AudioType.UNKNOWN;
        }
    }
}
