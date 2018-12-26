using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public Text scoreText;
    public Text leftKeyText;

    public int scoreValue = 0;
    public int leftKey = 0;

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
}
