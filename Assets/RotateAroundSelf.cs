using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{

    public float speed = 1f;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0)) return;

        transform.RotateAround(transform.position, Vector3.right, Time.deltaTime * speed);

    }
}
