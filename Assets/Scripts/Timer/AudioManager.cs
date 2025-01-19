using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip clip1;
    [SerializeField]
    private AudioClip clip2;
    public float SoundLevel = 1;
    private List<AudioSource> idleAudioSources1 = new List<AudioSource>();
    private List<AudioSource> idleAudioSources2 = new List<AudioSource>();

    public void PlayClip1(float volume)
    {
        PlayClip(clip1, idleAudioSources1, SoundLevel);
    }

    public void PlayClip2(float volume)
    {
        PlayClip(clip2, idleAudioSources2, SoundLevel);
    }

    private void PlayClip(AudioClip clip, List<AudioSource> idleAudioSources, float volume)
    {
        AudioSource audioSource;
        if (idleAudioSources.Count > 0)
        {
            audioSource = idleAudioSources[idleAudioSources.Count - 1];
            idleAudioSources.RemoveAt(idleAudioSources.Count - 1);
        }
        else
        {
            GameObject audioObject = new GameObject("Audio Source");
            audioObject.transform.parent = transform;
            audioSource = audioObject.AddComponent<AudioSource>();
        }
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        StartCoroutine(StopAndRecycle(audioSource, idleAudioSources, clip.length));
    }

    private IEnumerator StopAndRecycle(AudioSource audioSource, List<AudioSource> idleAudioSources, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
        idleAudioSources.Add(audioSource);
    }
}
