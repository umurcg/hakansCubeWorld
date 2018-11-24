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
    public float ascendSpeed = 1f;
    public float descendSpeed = 3f;
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

        moveDir += (ver * Vector3.forward*moveSpeed);

        charCont.Move(moveDir * Time.deltaTime);

    }

    public void waitForTransmission()
    {
        //Disable update call
        this.enabled = false;
        StartCoroutine(lerpPos(transform.position + Vector3.up*transmissionHeight,1f));
    }

    public void finishTransmission() {
        
        lerpPos(transform.position - Vector3.up * transmissionHeight, 1f,true);
    }

    public IEnumerator Tween(GameObject aim, float speed)
    {

        Vector3 initialPosition =transform.position;
        float ratio = 0;
        while (ratio < 1)
        {
            ratio += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(initialPosition, aim.transform.position, ratio);
            yield return 0;

        }
        //transform.position = aim.transform.position;
        yield break;
    }




    public IEnumerator ascentDescent(GameObject ascentObj, GameObject descentObj)
    {
        var obj=StartCoroutine(Tween(ascentObj,ascendSpeed));
        yield return obj;
        StartCoroutine(Tween(descentObj, descendSpeed));
        yield break;
    }


    public IEnumerator ascent()
    {
        this.enabled = false;

        float curHeight = 0;
        while (curHeight < transmissionHeight)
        {
            var delta = ascendSpeed * Time.deltaTime;
            transform.position += Vector3.up * delta;
            curHeight += delta;
            
            yield return 0;
        }

        yield break;
    }

    public IEnumerator descent()
    {
        float curHeight = 0;
        while (curHeight < transmissionHeight)
        {
            var delta = descendSpeed * Time.deltaTime;
            transform.position -= Vector3.up * delta;
            curHeight += delta;

            yield return 0;
        }

        this.enabled = true;

        yield break;
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

 
