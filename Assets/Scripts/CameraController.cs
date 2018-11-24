using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float runningZoffset=-5f;
    public float transitionZoffset = -10f;
    

    public float zoomOutSpeed = 1f;
    public float zoomInSpeed = 3f;

    public float yOffset = 5f;
    public bool transmissionMode = false;

    float zOffset;
    float transmissionLerpDelta = 0.1f;


    // Use this for initialization
    void Start () {
        zOffset = runningZoffset;
	}
	
	// Update is called once per frame
	void Update () {

        //Transmission mode
        //Camera will zoom out in this mode 
        if (transmissionMode && Mathf.Abs(transitionZoffset - zOffset) > transmissionLerpDelta)
        {
            zOffset = Mathf.Lerp(zOffset, transitionZoffset, Time.deltaTime * zoomOutSpeed);

        }
        else if (transmissionMode == false && zOffset != runningZoffset)
        {
            zOffset = Mathf.Lerp(zOffset, runningZoffset, Time.deltaTime * zoomInSpeed);
        }
     

        transform.position = player.transform.position + Vector3.forward * zOffset + Vector3.up * yOffset;
        transform.LookAt(player.transform.position);

	}
    


    IEnumerator lerpPos(Vector3 pos, float lerpSpeed)
    {
        var initalPos = transform.position;
        var ratio = 0f;

        while (ratio < 1)
        {
            ratio += Time.deltaTime * lerpSpeed;
            var newPos = Vector3.Lerp(initalPos, pos, ratio);
            transform.position = newPos;
        }

        transform.position = pos;

        yield break;
    }

    //IEnumerator shakeCamera(float shakeRatio, float shakeDuration, float shakeSpeed)
    //{
    //    var t = 0f;
    //    var right = true;

    //    while (t < shakeDuration)
    //    {
    //        if (right)
    //        {
    //            var upDir=transform.transform
    //        }

    //        yield return null;
    //    }

    //    yield break;

    //}

}
