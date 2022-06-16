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
    public GameObject gameOverPanel;


    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.GetInt("score") > 0)
        {
            score = PlayerPrefs.GetInt("score");
            scoreText.text = "x " + score.ToString();
        }

        Time.timeScale = 1;
    }

    public void GetCoin()
    {
        score++;
        scoreText.text = "x " + score.ToString();

        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
