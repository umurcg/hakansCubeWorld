using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController charCont;
    public float moveSpeed = 1f;
    public GameObject cubeWorld;
    public float jumpSpeed = 5f;

    public float gravity = 9f;

    public float transmissionHeight = 10f;

    // Use this for initialization
    void Start()
    {
        charCont = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        var moveDir = transform.forward * moveSpeed;

        //Apply gravity
        moveDir.y = moveDir.y - gravity;

        if (Input.GetButton("Jump"))
        {
            moveDir.y = jumpSpeed;
        }

        //var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        moveDir += (ver * Vector3.forward);

        charCont.Move(moveDir * Time.deltaTime);

    }

    public void waitForTransmission()
    {
        this.enabled = false;
        StartCoroutine(lerpPos(transform.position + Vector3.up*transmissionHeight,1f));
    }

    public void finishTransmission() {
        
        lerpPos(transform.position - Vector3.up * transmissionHeight, 1f,true);
    }

    IEnumerator lerpPos(Vector3 pos, float lerpSpeed, bool enableAfter=false)
    {
        var initalPos = transform.position;
        var ratio = 0f;

        while (ratio < 1)
        {
            ratio += Time.deltaTime * lerpSpeed;
            var newPos= Vector3.Lerp(initalPos, pos, ratio);
            transform.position = newPos;

            yield return null;
        }

        transform.position = pos;

        if (enableAfter)
            this.enabled = true;


        yield break;
    }

}

 
