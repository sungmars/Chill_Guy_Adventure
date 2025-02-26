using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource audioSource;

    public BGMController()
    {
        GameObject obj = new GameObject("Bgm");
        audioSource = obj.AddComponent<AudioSource>();
        audioSource.loop = true; //반복재생 시키키
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
