using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    private List<AudioSource> audioSources;

    public EnemyAudioController()
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
        GameObject obj = new GameObject("MobAudio");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        audioSources.Add(newSource);
        return newSource;
    }
}
