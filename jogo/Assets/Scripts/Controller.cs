using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public int score;
    public Text scoreText;

    public static Controller instance;
    private void Awake()
    {
        instance = this;
    }

    public void GetCoin()
    {
        score++;
        scoreText.text = "x " + score.ToString();
    }
}
