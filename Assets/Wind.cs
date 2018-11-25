using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    Ray ray;
    public float impact = 0.1f;
    public float impactDuration = 1f;
    float impactCounter = 0f;
    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ray = new Ray(transform.position, transform.forward);

	}
	
	// Update is called once per frame
	void Update () {

        if (impactCounter > 0)
        {   
            impactCounter -=Time.deltaTime;
            player.transform.position += transform.forward * impact;
            return;
        }

        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            
            if (hit.transform.gameObject == player)
            {
                impactCounter = impactDuration;
     
            }
            
        }

    }

    
}
