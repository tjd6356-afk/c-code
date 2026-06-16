using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 코루틴(IEnumerator) 사용을 위해 반드시 필요합니다!

public class DeathZone : MonoBehaviour
{
    [Header("UI 설정")]
    [SerializeField] private GameObject deadPanel; // 띄울 데드 패널 오브젝트

    [Header("시간 설정")]
    [SerializeField] private float restartDelay = 2.0f; // 재시작까지 걸릴 시간 (초 단위)

    private bool isDead = false; // 플레이어가 이미 죽었는지 체크 (중복 실행 방지)

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 부딪혔고, 아직 죽은 상태가 아니라면 실행
        if (other.CompareTag("Player") && !isDead)
        {
            isDead = true; // 죽음 상태로 변경
            StartCoroutine(PlayerDeathSequence()); // 사망 시퀀스(코루틴) 시작
        }
    }

    // 시간 지연을 처리하는 코루틴 함수
    private IEnumerator PlayerDeathSequence()
    {
        Debug.Log("플레이어 사망! 패널을 활성화합니다.");

        // 1. 숨겨두었던 데드 패널을 화면에 표시
        if (deadPanel != null)
        {
            deadPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Dead Panel이 인스펙터에 지정되지 않았습니다!");
        }

        // 2. 인스펙터에서 내가 지정한 시간(restartDelay)만큼 대기
        yield return new WaitForSeconds(restartDelay);

        // 3. 시간이 지나면 씬 재시작
        Debug.Log("지정한 시간이 지나 씬을 재시작합니다.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}