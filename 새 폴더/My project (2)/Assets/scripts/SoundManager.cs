using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Clips")]
    public AudioClip audioClip;    // 카드 클릭 효과음
    public AudioClip audioBGMClip; // 배경음악

    private AudioSource audioSource;
    private AudioSource audioSourceBGM;

    // 1. Awake는 Start보다 먼저 호출됩니다.
    void Awake()
    {
        // 싱글톤 설정: 인스턴스가 없으면 나를 할당
        if (instance == null)
        {
            instance = this;
            // 씬이 바뀌어도 배경음악이 끊기지 않게 유지하고 싶다면 아래 주석을 푸세요
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // 이미 인스턴스가 있다면 중복 생성된 것이므로 파괴
            Destroy(gameObject);
            return;
        }

        // AudioSource 초기화 (Awake에서 미리 만들어두는 게 안전합니다)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void PlayBGMSound()
    {
        if (audioBGMClip != null && audioSourceBGM != null)
        {
            audioSourceBGM.clip = audioBGMClip;
            audioSourceBGM.loop = true;
            audioSourceBGM.Play();
        }
        else
        {
            Debug.LogWarning("BGM 클립이 비어있습니다!");
        }
    }
}