using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardGame : MonoBehaviour
{
    [Header("Settings")]
    public GameObject cardPrefab;    // 카드 프리팹
    public Transform cardParent;     // 카드가 생성될 부모 객체
    public int totalCardCount = 12;  // 생성할 카드 총 개수 (2~20 사이)

    public List<Card> card = new List<Card>(); // 생성된 카드들을 담을 리스트
    public List<Sprite> sprites;

    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupBoard(totalCardCount);
    }

    public void SetupBoard(int count)
    {
        // 1. 기존 카드 제거 (초기화용)
        foreach (Card c in card)
        {
            if (c != null) Destroy(c.gameObject);
        }
        card.Clear();

        // 2. 홀수라면 짝수로 맞춤 (페어 게임이므로)
        if (count % 2 != 0) count--;
        if (count < 2) count = 2;
        if (count > 20) count = 20;

        totalCardCount = count;

        // 3. 프리팹 생성
        for (int i = 0; i < totalCardCount; i++)
        {
            GameObject newCardObj = Instantiate(cardPrefab, cardParent);
            Card newCard = newCardObj.GetComponent<Card>();

            // 카드 스크립트에 이 게임 매니저 정보를 전달
            newCard.cardGame = this;
            card.Add(newCard);
        }

        // 4. 게임 시작 로직 실행
        StartGame();
    }

    List<int> GeneratePairNumbers(int cardCount)
    {
        int pairCount = cardCount / 2;
        List<int> newCardNumbers = new List<int>();

        for(int i = 0; i < pairCount; ++i)
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
        List<int> randomPairNumbers = GeneratePairNumbers(card.Count);

        for(int i = 0; i < card.Count; ++i)
        {
            card[i].SetCardNumber(randomPairNumbers[i]);
            // 이미지 스프라이트 개수가 부족하지 않은지 체크 필요
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

        if(firstCard.cardNumber == secondCard.cardNumber)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;

            firstCard.ChangeColor(Color.magenta);
            secondCard. ChangeColor(Color.magenta);

            firstCard = null;
            secondCard = null;

            isChecking = false;
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
        if(isChecking)
        {
            return;
        }

        if (firstCard == null)
        {
            firstCard = card;
            firstCard.Flip(true);
        }

        else if(firstCard != card)
        {
            secondCard = card;
            secondCard.Flip(true);
        }

        if (firstCard != null && secondCard != null)
        {
            CheckCard();
        }
    }
    
}
