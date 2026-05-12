using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip audioClip;
    public AudioClip audioBGMClip;
    private AudioSource audioSource;
    private AudioSource audioSourceBGM;

    void Start()
    {
        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayBGMSound()
    {
        audioSourceBGM.clip = audioBGMClip;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }
    
}
