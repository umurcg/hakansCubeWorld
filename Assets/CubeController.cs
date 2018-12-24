using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public float rotateSpeed = 1f;

    public GameController gameController;

    	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);

        //Get session timer for setting timer with rotation left
        var currenSesionCube = gameController.getCurrentSesionCube();
        var curretTimer =currenSesionCube.GetComponent<Timer>();

        //Calculate time left from rotattion
        var angle=Vector3.Angle(currenSesionCube.transform.right, Vector3.right);
        curretTimer.setTimers((45.0f - angle) / 45);
        
    }

    //StartCoroutine(lerpRotation(Quaternion.(0, 0, 90), 1f));
    public IEnumerator lerpRotation(Quaternion aimRot, float lerpSpeed)
    {
        //var counter = 0f;

        var initalRot = transform.rotation;
        var ratio = 0f;

        while (ratio < 1)
        {
            ratio += Time.deltaTime * lerpSpeed;
            var newRot = Quaternion.Slerp(initalRot, aimRot, ratio);
            transform.rotation = newRot;

            //counter += Time.deltaTime;

            yield return null;
        }

        //print(counter);

        transform.rotation = aimRot;




        yield break;
    }

}
