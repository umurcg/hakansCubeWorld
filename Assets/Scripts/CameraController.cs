using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float runningZoffset=-5f;
    public float transitionZoffset = -10f;
    

    public float zoomOutSpeed = 1f;
    public float zoomInSpeed = 1f;

    public float yOffset = 5f;
    public bool transmissionMode = false;

    float zOffset;
    float transmissionLerpDelta = 0.1f;


    // Use this for initialization
    void Start () {
        zOffset = runningZoffset;
	}

    // Update is called once per frame
    void Update() {

        ////Transmission mode
        ////Camera will zoom out in this mode 

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

        //if (transmissionMode)
        //    return;

        //transform.position = player.transform.position + Vector3.forward * runningZoffset + Vector3.up * yOffset;
        //transform.LookAt(player.transform.position);



    }

    //public IEnumerator lerpToFarCenter()
    //{
    //    transmissionMode = true;
    //    StartCoroutine(lerpPos(new Vector3(0, 0, transitionZoffset),zoomOutSpeed));
    //    StartCoroutine(lerpLook(new Vector3(0, 0, transitionZoffset),new Vector3(0,0,0), zoomOutSpeed));
    //    print("far lerp finished");
    //    yield break;
    //}

    //public IEnumerator lerpToPalyer()
    //{
        
    //    StartCoroutine(lerpPos(player.transform.position + Vector3.forward * runningZoffset + Vector3.up * yOffset, zoomInSpeed));
    //    yield return StartCoroutine(lerpLook(new Vector3(0, player.transform.position.y+ yOffset, runningZoffset), player.transform.position, zoomInSpeed));
    //    transmissionMode = false;
    //    yield break;
    //}

    //IEnumerator lerpLook(Vector3 origin, Vector3 aim, float speed)
    //{
    //    Quaternion aimRot = Quaternion.LookRotation(aim - origin,Vector3.up);
    //    var initialRot = transform.rotation;
    //    float r = 0f;
    //    while (r < 1)
    //    {
    //        r += Time.deltaTime * speed;
    //        transform.rotation = Quaternion.Lerp(transform.rotation, aimRot, r);
    //        yield return null;
    //    }

    //    transform.rotation = aimRot;
    //    yield break;
        
        
    //}

    //IEnumerator lerpPos(Vector3 pos, float lerpSpeed)
    //{
    //    var initalPos = transform.position;
    //    var ratio = 0f;

    //    while (ratio < 1)
    //    {
    //        ratio += Time.deltaTime * lerpSpeed;
    //        var newPos = Vector3.Lerp(initalPos, pos, ratio);
    //        transform.position = newPos;
    //        yield return null;
    //    }

    //    transform.position = pos;

    //    yield break;
    //}

    

}
