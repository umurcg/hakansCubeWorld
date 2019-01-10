using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject restartButton;
    public GameObject continueButton;

    public void restart() {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
