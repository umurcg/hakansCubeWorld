using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float runningZoffset=-5f;
    public float transitionZoffset = -10f;

    float zOffset;
    public float transitionSpeed = 1f;

    public float yOffset = 5f;

    public bool transmissionMode = false;

	// Use this for initialization
	void Start () {
        zOffset = runningZoffset;
	}
	
	// Update is called once per frame
	void Update () {


        //print(transmissionMode);
        if (transmissionMode && Mathf.Abs(transitionZoffset-zOffset)>0.5f)
        {
            print(Mathf.Abs(transitionZoffset - zOffset));
            print("moving cam");
            zOffset= Mathf.Lerp(zOffset, transitionZoffset, Time.deltaTime * transitionSpeed);
        }else if(transmissionMode==false && zOffset != runningZoffset)
        {
            zOffset = Mathf.Lerp(zOffset, runningZoffset, Time.deltaTime * transitionSpeed);
        }
        else if(transmissionMode){
            transmissionMode = false;
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



}
