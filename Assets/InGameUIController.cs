using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public Text scoreText;
    public Text leftKeyText;
    public GameObject tutorial;
    public float tutorialTime = 10f;

    public int scoreValue = 0;
    public int leftKey = 0;

    public Image timerFill;

    public void setTimer(float ratio)
    {
        timerFill.fillAmount =ratio;
    }

    public void setScore(int score)
    {
        scoreText.text = score.ToString();
        scoreValue = score;
    }
    public void setKey(int key)
    {
        leftKeyText.text = key.ToString();
        leftKey = key;
    }

    private void Update()
    {
        if (tutorialTime > 0)
        {
            tutorialTime -= Time.deltaTime;
            if (tutorialTime < 0)
                tutorial.gameObject.SetActive(false);
        }

    }

}
