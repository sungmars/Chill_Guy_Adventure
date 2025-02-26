using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();
    }
        
    public void PlaySound(AudioClip clip)
    {
        AudioSource source = GetAvailableAudioSource();
        if(source != null)
        {
            source.PlayOneShot(clip);
        }
    }
    private AudioSource GetAvailableAudioSource()
    {
        foreach(var source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        GameObject obj = new GameObject("EnemyAudio");
        obj.transform.parent = this.transform;
        AudioSource newSource = obj.AddComponent<AudioSource>();
        audioSources.Add(newSource);
        return newSource;
    }
    public void SetVolume(float volume)
    {
        // 리스트의 각 AudioSource에 대해 볼륨 설정
        foreach (var source in audioSources)
        {
            if (source != null)
                source.volume = volume;
        }
    }
}
