using UnityEngine;
using UnityEngine.SceneManagement; // 씬 재시작을 위해 필요합니다.
using System.Collections; // 코루틴(IEnumerator) 사용을 위해 필요합니다!

public class Goal : MonoBehaviour
{
    [Header("UI 설정")]
    public GameObject clearText; // "스테이지 클리어" 텍스트 또는 패널 오브젝트

    [Header("시간 설정")]
    [SerializeField] private float clearDelay = 3.0f; // 클리어 화면을 보여줄 시간 (초 단위)

    private bool isCleared = false; // 중복 실행 방지용 플래그

    private void Start()
    {
        if (clearText != null)
        {
            clearText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 골인 지점에 들어왔고, 아직 클리어 상태가 아니라면 실행
        if (other.CompareTag("Player") && !isCleared)
        {
            isCleared = true; // 클리어 상태로 변경 (연속 트리거 방지)
            StartCoroutine(ClearSequence()); // 클리어 시퀀스 시작
        }
    }

    // 시간 지연을 처리하는 코루틴 함수
    private IEnumerator ClearSequence()
    {
        Debug.Log("축하합니다! 스테이지를 클리어했습니다.");

        // 1. 클리어 패널(텍스트) 활성화
        if (clearText != null)
        {
            clearText.SetActive(true);
        }

        // 2. 인스펙터에서 지정한 시간(clearDelay)만큼 대기
        yield return new WaitForSeconds(clearDelay);

        // 3. 시간이 지나면 현재 씬 재시작
        Debug.Log("지정한 시간이 지나 씬을 재시작합니다.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}