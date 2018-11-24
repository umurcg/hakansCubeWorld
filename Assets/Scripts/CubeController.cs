using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public float rotateSpeed = 1f;
    public float seasonTransitionSpeed = 1f;

	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);

	}

    public IEnumerator setSeasonRotation(Vector3 seasonDirection)
    {
        this.enabled = false;
        yield return  StartCoroutine(lerpRotation(Quaternion.Euler(seasonDirection), seasonTransitionSpeed));
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

            print("lerping");
            print(newRot);

            yield return null;
        }

        transform.rotation = aimRot;

        yield break;
    }

}
