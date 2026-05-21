using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardGame : MonoBehaviour
{
    [Header("Settings")]
    public GameObject cardPrefab;
    public Transform cardParent;
    public int totalCardCount = 12;

    [Header("Level Info")]
    public int currentLevelNumber = 1; // ⭐ 현재 레벨 번호 (인펙터에서 1, 2, 3 등으로 지정)

[Header("Timer Connection")]
    public TimerManager timerManager; // ⭐ 인스펙터에서 TimerManager를 연결하는 칸
    public float levelLimitTime = 60f; // ⭐ 이 레벨의 제한 시간 설정

    public List<Card> card = new List<Card>();
    public List<Sprite> sprites;

    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;
    private bool isGameOver = false; // ⭐ 게임 오버 상태 체크 변수

    // ⭐ 게임 클리어 체크를 위한 변수들
    private int matchedPairs = 0;
    

    void Start()
    {
        SetupBoard(totalCardCount);
    }

    // ⭐ 매 프레임 타이머 가동 (점수 산출용)
    

    public void SetupBoard(int count)
    {
        foreach (Card c in card)
        {
            if (c != null) Destroy(c.gameObject);
        }
        card.Clear();

        if (count % 2 != 0) count--;
        if (count < 2) count = 2;
        if (count > 20) count = 20;

        totalCardCount = count;
        matchedPairs = 0; // 초기화
        isGameOver = false; // 초기화

        for (int i = 0; i < totalCardCount; i++)
        {
            GameObject newCardObj = Instantiate(cardPrefab, cardParent);
            Card newCard = newCardObj.GetComponent<Card>();
            newCard.cardGame = this;
            card.Add(newCard);
        }

        StartGame();
    }

    List<int> GeneratePairNumbers(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNumbers = new List<int>();

        for (int i = 0; i < pairCount; ++i)
        {
            newCardNumbers.Add(i);
            newCardNumbers.Add(i);
        }

        for (int i = newCardNumbers.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            int temp = newCardNumbers[i];
            newCardNumbers[i] = newCardNumbers[rnd];
            newCardNumbers[rnd] = temp;
        }

        return newCardNumbers;
    }

    private void StartGame()
    {
        SoundManager.instance.PlayBGMSound();
        List<int> randomPairNumbers = GeneratePairNumbers(card.Count);

        for (int i = 0; i < card.Count; ++i)
        {
            card[i].SetCardNumber(randomPairNumbers[i]);
            if (randomPairNumbers[i] < sprites.Count)
            {
                card[i].SetImage(sprites[randomPairNumbers[i]]);
            }
            card[i].isFront = false;
        }

        // ⭐ [추가] 게임판 세팅이 끝나면 타이머를 작동시킵니다.
        if (timerManager != null)
        {
            timerManager.StartTimer(levelLimitTime, this);
        }
    }

    private void CheckCard()
    {
        isChecking = true;

        if (firstCard.cardNumber == secondCard.cardNumber)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;

            firstCard.ChangeColor(Color.magenta);
            secondCard.ChangeColor(Color.magenta);

            firstCard = null;
            secondCard = null;

            isChecking = false;

            // ⭐ [추가] 카드 매칭 성공 카운트 증가 및 클리어 조건 체크
            matchedPairs++;
            if (matchedPairs == totalCardCount / 2)
            {
                OnLevelClear();
            }
        }
        else
        {
            Invoke("HideCard", 1.0f);
        }
    }

    private void HideCard()
    {
        firstCard.isFront = false;
        secondCard.isFront = false;
        firstCard.Flip(false);
        secondCard.Flip(false);
        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    public void OnClickCard(Card card)
    {
// ⭐ 시간이 끝났거나 검사 중이면 터치 불가
        if(isChecking || isGameOver) return;

        if (firstCard == null)
        {
            firstCard = card;
            firstCard.Flip(true);
            SoundManager.instance.PlaySound();
        }
        else if (firstCard != card)
        {
            secondCard = card;
            secondCard.Flip(true);
            SoundManager.instance.PlaySound();
        }

        if (firstCard != null && secondCard != null)
        {
            CheckCard();
        }
    }

    // ⭐ [새로 추가된 함수] 레벨 클리어 시 호출됨
    private void OnLevelClear()
    {
        Debug.Log($"레벨 {currentLevelNumber} 클리어!");

        // ⭐ 1. 성공했으니 타이머를 먼저 멈춥니다.
        if (timerManager != null)
        {
            timerManager.StopTimer();
        }
        // ⭐ 2. 점수 계산 메커니즘 변경: 남은 시간이 많을수록 고득점!
        float remainingTime = (timerManager != null) ? timerManager.GetRemainingTime() : 0f;
        int score = Mathf.Max(100, Mathf.RoundToInt(remainingTime * 100)); // 남은 시간 1초당 100점
        
        // 기존 최고 점수보다 높을 때만 갱신하여 저장
        int oldScore = PlayerPrefs.GetInt($"LevelScore_{currentLevelNumber}", 0);
        if (score > oldScore)
        {
            PlayerPrefs.SetInt($"LevelScore_{currentLevelNumber}", score);
        }

        // 2. 다음 레벨 해제 처리 (현재 잠금 해제된 최대 레벨 기록)
        int currentMaxUnlocked = PlayerPrefs.GetInt("MaxUnlockedLevel", 1);
        if (currentLevelNumber >= currentMaxUnlocked)
        {
            // 현재 깬 레벨이 최고 레벨이라면 다음 레벨 번호를 저장
            PlayerPrefs.SetInt("MaxUnlockedLevel", currentLevelNumber + 1);
        }

        // 데이터 즉시 하드디스크에 저장 및 로비로 이동
        PlayerPrefs.Save();

        // 3초 뒤에 로비 씬으로 복귀 (UIManager의 기능 활용)
        Invoke("GoToLobby", 2.0f);
    }

    // ⭐ [새로 추가된 함수] 시간이 0이 되면 TimerManager가 나를 원격 호출함
    public void OnGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        Debug.Log("💥 게임 오버! 로비로 돌아갑니다.");

        // 모든 카드를 다시 뒷면으로 flip 해주는 연출을 넣어도 좋습니다.
        foreach (Card c in card)
        {
            if (c != null && !c.isMatched)
            {
                c.Flip(false);
            }
        }

        // 실패했으니 2초 뒤 짤막하게 로비로 쫓겨납니다.
        Invoke("GoToLobby", 2.0f);
    }

    private void GoToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("loby");
    }
}