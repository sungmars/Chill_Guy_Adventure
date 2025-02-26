using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
