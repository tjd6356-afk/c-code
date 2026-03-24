using UnityEngine; 
using TMPro;

public class Cards : MonoBehaviour
{
        public TextMeshProUGUI text;
        public int cardNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        
        text.text = Random.Range(0, 10).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 2, 0);
    }

    
}
