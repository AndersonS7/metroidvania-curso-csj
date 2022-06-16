using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int heartsCount;

    public Image[] hearts;
    public Sprite heart;
    public Sprite noHeart;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //controla a vida
            if (i < health)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = noHeart;
            }

            //controla a quantidade de corações que vai aparecer
            if (i < heartsCount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
