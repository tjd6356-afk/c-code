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

        [Header("Card Images")]
        public Sprite backSprite;     // 인스펙터에서 넣을 카드 뒷면 이미지
        private Sprite frontSprite;   // 게임 시작 시 할당받을 앞면 이미지

        private Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
        private Quaternion originRotation = Quaternion.Euler(0, 0, 0);
        private Color originalColor;
        private Image cardImage;

        [HideInInspector] public CardGame cardGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
        cardImage = GetComponent<Image>();
        originalColor = cardImage.color;

        if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        // 1. 부드러운 회전 처리
        Quaternion targetRotation = isFront ? originRotation : flipRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        // 2. 현재 카드의 Y축 회전 각도를 가져옵니다. (0 ~ 360도)
        float currentYAngle = transform.eulerAngles.y;

        // 3. 각도가 90도 ~ 270도 사이라면 카드가 뒤집힌 상태로 간주
        if (currentYAngle > 90f && currentYAngle < 270f)
        {
            // 뒷면 보여주기
            if (cardImage != null && backSprite != null)
                cardImage.sprite = backSprite;

            // 텍스트(숫자) 숨기기
            if (text != null)
                text.enabled = false;
        }
        else
        {
            // 앞면 보여주기
            if (cardImage != null && frontSprite != null)
                cardImage.sprite = frontSprite;

            // 텍스트(숫자) 보이기
            if (text != null)
                text.enabled = true;
        }


    }

    public void ClickCard()
    {
        if(!isMatched && cardGame != null)
        {
            cardGame.OnClickCard(this);
        }
    }

    public void Flip(bool front)
    {
        isFront = front;
    }

    public void SetCardNumber(int newNumber)
    {
        cardNumber = newNumber;
        if (text != null) text.text = newNumber.ToString();
    }

    public void ChangeColor(Color newColor)
    {
        if (cardImage != null) cardImage.color = newColor;
    }

    public void ResetColor()
    {
        if (cardImage != null) cardImage.color = originalColor;
    }

    public void SetImage(Sprite sprite)
    {
        // 전달받은 이미지를 '앞면 이미지'로 저장해 둡니다.
        frontSprite = sprite;
    }
}
