using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    public BossAudioController()
    {
        GameObject obj = new GameObject("Boss Audio");
        audioSource = obj.AddComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
