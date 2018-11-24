using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public CubeController cubeWorld;
    public PlayerController player;
    public CameraController cam;

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
            cam.transmissionMode = true;
            cubeWorld.enabled = false;
            StartCoroutine(cubeWorld.lerpRotation(Quaternion.Euler(0, 0, 90), 1f));
            //player.waitForTransmission();

            player.transform.SetParent(cubeWorld.transform);

        }

    }


    

}
