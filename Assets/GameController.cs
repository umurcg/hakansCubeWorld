using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    RandomProbeGenerator probeGenerator;
    public GameObject menu;

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
            menu.SetActive(true);
            Time.timeScale = 0;
            
        }

	}

    public void nextSesion()
    {
        if ((int)currentSesion + 1 == (int)RandomProbeGenerator.sesions.COUNT)
        {
            levelUp();
            currentSesion = RandomProbeGenerator.sesions.spring;
        }
        else
        {
            currentSesion += 1;
        }

        //Update reverse sesion
        var reverseSesion = ((int)currentSesion >= 2) ? currentSesion - 2 : currentSesion + 2;
        probeGenerator.updateSeasonProbes(reverseSesion);
    }
    

    public void levelUp()
    {

    }
}
