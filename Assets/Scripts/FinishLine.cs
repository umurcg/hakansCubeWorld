using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public CubeController cubeWorld;
    public PlayerController player;
    public CameraController cam;
    public GameObject ascentObject;
    public GameObject descentObject;

    

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
            //Enable cameras transmission mode
            cam.transmissionMode = true;

            //Ascent player for transition
            //Set parent of player as cubewolrd hile cube world is gonna be rotated too
            //player.transform.SetParent(cubeWorld.transform);
            StartCoroutine(player.ascentDescent(ascentObject,descentObject));

            //cubeWorld.enabled = false;
            //StartCoroutine(cubeWorld.lerpRotation(Quaternion.Euler(0, 0, 90), 1f));
            StartCoroutine(cubeWorld.setSeasonRotation(transform.localRotation.eulerAngles+new Vector3(0,0,90)));



        }

    }


    

}
