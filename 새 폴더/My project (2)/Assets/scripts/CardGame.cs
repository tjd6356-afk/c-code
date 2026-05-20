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

    public List<Card> card = new List<Card>();
    public List<Sprite> sprites;

    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;

    // ⭐ 게임 클리어 체크를 위한 변수들
    private int matchedPairs = 0;
    private float levelTimer = 0f; // 점수 계산용 타이머

    void Start()
    {
        SetupBoard(totalCardCount);
    }

    // ⭐ 매 프레임 타이머 가동 (점수 산출용)
    void Update()
    {
        levelTimer += Time.deltaTime;
    }

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
        levelTimer = 0f;  // 초기화

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
        if (isChecking) return;

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

        // 1. 점수 계산 (빨리 깰수록 높은 점수, 최소 100점 보장)
        int score = Mathf.Max(100, 5000 - Mathf.RoundToInt(levelTimer * 10));

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

    private void GoToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("loby");
    }
}