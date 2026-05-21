using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("UI 연결")]
    public Image timerGauge;       
    public TextMeshProUGUI timerText; 

    [Header("설정")]
    public float totalTime = 60f;
    private float currentTime;
    private bool isTimerRunning = false;

    private CardGame cardGame; // ⭐ 카드 게임 매니저 참조용

    // ⭐ [새로 추가] 카드 게임이 시작될 때 타이머를 원격 제어하기 위한 함수
    public void StartTimer(float time, CardGame gameInstance)
    {
        totalTime = time;
        currentTime = totalTime;
        cardGame = gameInstance;
        isTimerRunning = true;
    }

    // ⭐ [새로 추가] 게임 클리어 시 타이머를 멈추기 위한 함수
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // ⭐ [새로 추가] 남은 시간을 가져오기 위한 함수 (점수 계산용)
    public float GetRemainingTime()
    {
        return currentTime;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                currentTime = 0;
                isTimerRunning = false;
                UpdateTimerUI();
                
                Debug.Log("⌛ 제한 시간 종료!");
                
                // ⭐ 시간이 끝나면 카드 게임 매니저에게 게임 오버를 알립니다.
                if (cardGame != null)
                {
                    cardGame.OnGameOver();
                }
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerGauge != null)
        {
            timerGauge.fillAmount = currentTime / totalTime;

            if (timerGauge.fillAmount > 0.5f)
                timerGauge.color = Color.green;
            else if (timerGauge.fillAmount > 0.2f)
                timerGauge.color = Color.yellow;
            else
                timerGauge.color = Color.red;
        }

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}