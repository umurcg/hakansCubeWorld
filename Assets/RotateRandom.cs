using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandom : MonoBehaviour {

    public float maxRot = 10f;
    public float minRot = 1f;

    float rot;

    bool right = false;

	// Use this for initialization
	void Start () {
        rot = Random.Range(minRot, maxRot);
        if (Random.Range((int)0, (int)2) == 0)
            right = true;
	}
	
	// Update is called once per frame
	void Update () {

        var dir = 1;
        if (right)
            dir = -1;

        var angle = dir * Time.deltaTime * rot;
        transform.RotateAround(transform.position, transform.forward, angle);
	}
}
