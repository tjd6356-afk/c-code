using UnityEngine; 
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
        public TextMeshProUGUI text;
        public int cardNumber;
        public float RotationSpeed = 10.0f;
        public bool isFront = true;
        public bool isMatched = false;
        private Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
        private Quaternion originRotation = Quaternion.Euler(0, 0, 0);
        public CardGame cardGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
        if(isFront)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, RotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRotation, RotationSpeed * Time.deltaTime);
        }
        
        
    }

    public void ClickCard()
    {
        if(!isMatched)
        {
            cardGame.OnClickCard(this);
            isFront = !isFront;
        }
    }

    public void SetCardNumber(int newNumber)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        cardNumber = newNumber;
        text.text = newNumber.ToString();
    }
    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }
}
