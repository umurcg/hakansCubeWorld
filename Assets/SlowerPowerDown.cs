using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerPowerDown : MonoBehaviour {

    public float impact = 0.1f;
    public float impactDuration = 1f;
    float impactCounter = 0f;
    public GameObject player;
    public bool destroyAfterTrigger = false;
    public float rollSpeed = 1f;
    public GameObject parent;


    // Use this for initialization
    void Start () {
        parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (impactCounter > 0)
        {
            impactCounter -= Time.deltaTime;
            player.transform.position -= player.transform.forward * impact;
            if (destroyAfterTrigger)
            {
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject,impactDuration);
            }
            
        }

        transform.parent = null;
        transform.Rotate(Vector3.right, Time.deltaTime * rollSpeed);
        //Debug.Break();
        transform.parent = (parent.transform);
        //transform.localRotation *= Quaternion.Euler(0, 0, Time.deltaTime * rollSpeed);
        //transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rollSpeed);
        //transform.position += transform.forward * rollSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {   
            
            impactCounter = impactDuration;
        }
    }


}
