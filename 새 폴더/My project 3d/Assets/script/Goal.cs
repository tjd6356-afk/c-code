using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject clearText; // "게임 클리어" 텍스트 오브젝트

    private void Start()
    {
        clearText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clearText.SetActive(true);
        }
    }
}