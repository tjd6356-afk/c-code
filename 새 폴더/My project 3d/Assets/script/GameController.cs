using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TextMeshProUGUI text;

    public GameObject clear;

    private int score = 0;





    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int value)
    {
        score += value;

        text.text = "Score : " + score.ToString();
    }

    public void ClearGame()
    {
        clear.SetActive(true);
    }
}
