using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPower : MonoBehaviour {

    public GameObject player;
    public float dashDistance = 5f;
    public GameObject obstacleExplotionParticlePrefab;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            dash();
    }

    void dash()
    {
        var endPoint = transform.position + dashDistance * transform.forward;
       
        var ray = new Ray(transform.position, dashDistance * transform.forward);
        RaycastHit[] hits= Physics.RaycastAll(ray, dashDistance);
        Debug.DrawRay(ray.origin, dashDistance * transform.forward);
        Debug.DrawRay(ray.origin, endPoint);
        Debug.Break();
        foreach (var hit in hits)
        {
            print(hit.transform.tag);
            if (hit.transform.tag == "Obstaciles")
            {
                Destroy(hit.transform.gameObject);

            }else if (hit.transform.tag == "FinishLine")
            {
                endPoint = hit.point;
            }
        }

        player.transform.position = endPoint;
    }

    


}
