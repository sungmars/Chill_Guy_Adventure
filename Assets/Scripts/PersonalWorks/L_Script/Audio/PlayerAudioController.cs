using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    public PlayerAudioController()
    {
        GameObject obj = new GameObject("PlayerAudio");
        audioSource = obj.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
