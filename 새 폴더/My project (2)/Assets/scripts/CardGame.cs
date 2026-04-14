using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CardGame : MonoBehaviour
{
    public List<Card> card;
    public List<Sprite> sprites;
    private Card firstCard = null;
    private Card secondCard = null;
    private bool isChecking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            card[i].SetImage(sprites[randomPairNumbers[i]]);
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
