using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController charCont;
    public float moveSpeed = 1f;
    public GameObject cubeWorld;
    public float jumpSpeed = 5f;
    public float jumpDuration = 1f;
    float jumpTimer = 0f;

    public float gravity = 9f;

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

        var moveDir = Vector3.zero;



        if (charCont.isGrounded)
        {
            moveDir+=transform.forward* moveSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                //StartCoroutine(jump());
                jumpTimer = jumpDuration;
            }

            //var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");

            moveDir += (ver * Vector3.forward*moveSpeed);

        }
        else
        {
            moveDir.y = moveDir.y - gravity;
        }

        if (jumpTimer > 0)
        {
            moveDir.y = moveDir.y +jumpSpeed;
            jumpTimer -=Time.deltaTime;
        }
        else
        {
            //Apply gravity
            moveDir.y = moveDir.y - gravity;
        }
        

        charCont.Move(moveDir * Time.deltaTime);
        


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


    IEnumerator jump()
    {
        var t = 0f;
        while (t < jumpDuration)
        {
            t += Time.deltaTime * jumpSpeed;
            charCont.Move(transform.up *Time.deltaTime * jumpSpeed);
            yield return null; 
        }
        yield break;

    }

    public IEnumerator ascentDescent(GameObject ascentObj, GameObject descentObj)
    {
        this.enabled = false;
        var obj=StartCoroutine(Tween(ascentObj,ascendSpeed));
        yield return obj;
        yield return StartCoroutine(Tween(descentObj, descendSpeed));
        this.enabled = true;
        yield break;
    }




    //StartCoroutine(lerpRotation(Quaternion.(0, 0, 90), 1f));
    public IEnumerator lerpRotation(Quaternion aimRot, float lerpSpeed)
    {

        var initalRot = transform.rotation;
        var ratio = 0f;

        while (ratio < 1)
        {
            ratio += Time.deltaTime * lerpSpeed;
            var newRot = Quaternion.Slerp(initalRot, aimRot, ratio);
            transform.rotation = newRot;

            yield return null;
        }

        transform.rotation = aimRot;

        yield break;
    }

}






