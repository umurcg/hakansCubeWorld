using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public CubeController cubeWorld;
    public PlayerController player;
    public CameraController cam;
    public GameObject ascentObject;
    public GameObject descentObject;
    public GameController gameController;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject==player.gameObject)
        {
            //cam.transmissionMode = true;
            //StartCoroutine(player.ascentDescent(ascentObject, descentObject));
            //StartCoroutine(cubeWorld.setSeasonRotation(transform.localRotation.eulerAngles + new Vector3(0, 0, 90)));
            gameController.nextSesion();
            StartCoroutine(nextSessiong());
        }

    }


    IEnumerator nextSessiong()
    {

        this.enabled = false;
        //Enable cameras transmission mode
        cam.transmissionMode = true;

        //Ascent player for transition
        //Set parent of player as cubewolrd hile cube world is gonna be rotated too
        
        player.enabled = false;
        yield return StartCoroutine(player.Tween(ascentObject, player.ascendSpeed));
        

        player.transform.SetParent(ascentObject.transform);

        cubeWorld.enabled = false;
        var delta = cubeWorld.transform.rotation.eulerAngles.z % 90;

        StartCoroutine(cubeWorld.lerpRotation(Quaternion.Euler(cubeWorld.transform.localRotation.eulerAngles+new Vector3(0, 0, 90-delta)), 1f));
        yield return StartCoroutine(player.lerpRotation(Quaternion.Euler(0,90,0), 1f));
        cubeWorld.enabled = true;
   

        player.transform.SetParent(null);

        cam.transmissionMode = false;
        yield return StartCoroutine(player.Tween(descentObject, player.descendSpeed));
        player.enabled = true;

        //yield return StartCoroutine(player.Tween(descentObject, player.descendSpeed));

        yield break;
    }



}
