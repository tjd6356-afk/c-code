using UnityEngine; 
using TMPro;

public class Cards : MonoBehaviour
{
        public TextMeshProUGUI text;
        public int cardNumber;
        public float RotationSpeed = 10.0f;
        public bool isClick = false;
        private Quaternion flipRotation = Quaternion.Euler(0, 180f, 0);
        private Quaternion originRotation = Quaternion.Euler(0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        
        cardNumber = Random.Range(0, 10);

        text.text = cardNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isClick)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRotation, RotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, RotationSpeed * Time.deltaTime);
        }
        
        
    }

    public void ClickCard()
    {
        isClick = !isClick;
    }

}
