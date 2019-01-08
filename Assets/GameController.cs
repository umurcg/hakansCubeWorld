using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float speedPercStep = 0.1f;

    public float minCubeSpeed = 2;
    public float minHakanSpeed = 7.5f;

    public float maxCubeSpeed = 60f;
    public float maxHakanSpeed = 15f;
    public int maxLevel = 15;

    public CubeController cubeController;
    public PlayerController playerController;

    public SoundController soundController;

    public RandomProbeGenerator probeGenerator;
    public PowerObjectGenerator powerGenerator;
    public MenuController menu;

    public WallController wallController;

    public AudioClip gameOverSound;

    public int level = 1;
    public RandomProbeGenerator.sesions currentSesion = RandomProbeGenerator.sesions.spring;

    public InGameUIController uiController;

    public float fallAngle = 45f;

    // Use this for initialization
    void Start () {
        probeGenerator = GetComponent<RandomProbeGenerator>();
        powerGenerator = GetComponent<PowerObjectGenerator>();
        wallController = GetComponent<WallController>();

        wallController.createWall(currentSesion);
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

        ////First reset timer of current sesion
        //if(currentSesion>)


        if ((int)currentSesion + 1 == (int)RandomProbeGenerator.sesions.COUNT)
        {           
            currentSesion = RandomProbeGenerator.sesions.spring;
        }
        else
        {
            currentSesion += 1;
        }

        //Update reverse sesion
        var reverseSesion = geReverseSession();
        probeGenerator.updateSeasonProbes(reverseSesion);

        wallController.createWall(currentSesion);

        soundController.changeMusic();
        levelUp();
    }
    

    public void levelUp()
    {
        level++;

        uiController.setScore(level-1);

        var speedLevel = Mathf.Clamp(level, 0, maxLevel);              
        var speedRatio = (float)speedLevel / (float)maxLevel;
        cubeController.rotateSpeed = Mathf.Lerp(minCubeSpeed,maxCubeSpeed,speedRatio);
        playerController.moveSpeed = Mathf.Lerp(minHakanSpeed, maxHakanSpeed, speedRatio);

        //cubeController.rotateSpeed *= cubeController.rotateSpeed * (1 + speedPercStep);
        //playerController.moveSpeed *= playerController.moveSpeed * (1 + speedPercStep);

        probeGenerator.increaseNumberOfGroup();
        powerGenerator.spawnPowerDowns();
        powerGenerator.spawnPowerUp();
        powerGenerator.spawnWind();
    }

    public void lost()
    {
        menu.gameObject.SetActive(true);
        menu.restartButton.SetActive(true);
        menu.continueButton.SetActive(false);
        soundController.createSoundEffect(gameOverSound);

    }

    public RandomProbeGenerator.sesions geReverseSession()
    {
        var reverseSesion = ((int)currentSesion >= 2) ? currentSesion - 2 : currentSesion + 2;
        return reverseSesion;
    }

    public GameObject getCurrentSesionCube()
    {
        return probeGenerator.getSessionCube(currentSesion);
    }
    
    public Vector3 getForwardDirection()
    {
        var sesionCube = getCurrentSesionCube();
        var dir= sesionCube.transform.right;
        return dir;
    }

    public Vector3 getUpDirection()
    {
        var sesionCube = getCurrentSesionCube();
        var dir = sesionCube.transform.up;
        return dir;
    }

    public float getCubeAngle()
    {
        var forwardVector = getForwardDirection();
        var angle= Vector3.Angle(forwardVector, Vector3.right);
        print(angle);
        return angle;
    }

}
