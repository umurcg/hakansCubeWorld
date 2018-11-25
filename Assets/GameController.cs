using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float speedPercStep = 0.1f;

    public CubeController cubeController;
    public PlayerController playerController;


    RandomProbeGenerator probeGenerator;
    public MenuController menu;

    public int level = 1;
    public RandomProbeGenerator.sesions currentSesion = RandomProbeGenerator.sesions.spring;

	// Use this for initialization
	void Start () {
        probeGenerator = GetComponent<RandomProbeGenerator>();	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }

	}

    public void nextSesion()
    {
        if ((int)currentSesion + 1 == (int)RandomProbeGenerator.sesions.COUNT)
        {
            
            currentSesion = RandomProbeGenerator.sesions.spring;
        }
        else
        {
            currentSesion += 1;
        }

        //Update reverse sesion
        var reverseSesion = ((int)currentSesion >= 2) ? currentSesion - 2 : currentSesion + 2;
        probeGenerator.updateSeasonProbes(reverseSesion);

        levelUp();
    }
    

    public void levelUp()
    {
        cubeController.rotateSpeed *= 1 + speedPercStep;
        playerController.moveSpeed *= 1 + speedPercStep;
        probeGenerator.increaseNumberOfGroup();
    }

    public void lost()
    {
        menu.gameObject.SetActive(true);
        menu.restartButton.SetActive(true);
        menu.continueButton.SetActive(false);

    }
}
