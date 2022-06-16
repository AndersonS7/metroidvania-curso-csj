using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int score;
    public Text scoreText;

    public static Controller instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetCoin()
    {
        score++;
        scoreText.text = "x " + score.ToString();
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(1);
    }
}
