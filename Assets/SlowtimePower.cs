using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowtimePower : MonoBehaviour {

    public float duration=3f;
    public float timeScale = 0.5f;
    public float scaleSpeed = 1f;
    public GameObject player;

    public GameObject particlePrefab;

	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(slowMotion());
            this.enabled = false;
            


            particlePrefab.GetComponent<ParticleSystem>().Play();
            particlePrefab.transform.SetParent(player.transform);
            Destroy(particlePrefab, 1f);

            StartCoroutine(scaleToZero());
        }
    }
    

    IEnumerator scaleToZero()
    {
        var initScale = transform.localScale;
        var aimScale = Vector3.zero;

        var r = 0f;
        while (r < 1)
        {
            r += Time.deltaTime*scaleSpeed;
            transform.localScale = Vector3.Lerp(initScale, aimScale, r);


            yield return null;

        }

        yield break;

    }

    IEnumerator slowMotion()
    {
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;

        Destroy(gameObject);
        yield break;
    }
}
