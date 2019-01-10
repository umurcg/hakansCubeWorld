using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController charCont;
    Animator anim;
    SoundController soundController;

    public GameController gameController;
    public GameObject cubeWorld;

    public float moveSpeed = 1f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;


    public AudioClip[] jumpSounds;
    
    //For rotation mode
    public float ascendSpeed = 1f;
    public float descendSpeed = 3f;

    float _jumpVelocity;

    public LayerMask groundLayer;
    public GameObject groundChecker;
    public float groundDistance;

    

    // Use this for initialization
    void Start()
    {
        charCont = gameObject.GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        

        var forwardDirection = gameController.getForwardDirection();
        var upwardDirection = gameController.getUpDirection();


        var cubeAngle = gameController.getCubeAngle();
        if (cubeAngle >gameController.fallAngle)
        {
            charCont.Move(gravity * Time.deltaTime*forwardDirection*2);
            anim.SetBool("Jump", true);
            return;
        }

        Vector3 walkVector = Vector3.zero;

        var moveDir = Vector3.zero;

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");


        walkVector += forwardDirection * hor + Vector3.forward * ver;
        if(walkVector.magnitude>1)
            walkVector =walkVector.normalized;
        walkVector *= moveSpeed;              
            
        transform.LookAt(transform.position + walkVector, upwardDirection);

        var isGrounded = Physics.CheckSphere(groundChecker.transform.position, groundDistance, groundLayer, QueryTriggerInteraction.Ignore);
  

        //If character is in ground 
        if (isGrounded && _jumpVelocity<=0)
        {                    
            if (ver != 0 || hor != 0)
                anim.SetBool("Walking", true);
            else
                anim.SetBool("Walking", false);

            anim.SetBool("Jump", false);

            if (_jumpVelocity < 0)
                _jumpVelocity = 0;

            if (Input.GetButtonDown("Jump"))
            {
                _jumpVelocity += Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
                        
        }
        else {
            
            //Apply gravity
            _jumpVelocity += gravity*Time.deltaTime;

            anim.SetBool("Jump", true);
            
        }

        var verticalVelocity = (_jumpVelocity + gravity * Time.deltaTime)*upwardDirection;
        charCont.Move( (verticalVelocity+walkVector) * Time.deltaTime);



    }

    void OnDisable()
    {
        anim.SetBool("Jump", true);
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


    public IEnumerator lerpPos(Vector3 aim, float speed)
    {

        Vector3 initialPosition = transform.position;
        float ratio = 0;
        while (ratio < 1)
        {
            ratio += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(initialPosition, aim, ratio);
            
            yield return 0;

        }
        //transform.position = aim.transform.position;
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






