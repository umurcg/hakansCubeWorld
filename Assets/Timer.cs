using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject timerCylinderPrefab;
    public GameObject timerPS;

    GameObject upperTimer;
    GameObject downTimer;

    GameObject upperParticle;
    GameObject downParticle;



    // Start is called before the first frame update
    void Start()
    {
        createTimerObjects();



    }
    
    void createTimerObjects() {
 
        upperTimer = Instantiate<GameObject>(timerCylinderPrefab);
        upperParticle = Instantiate<GameObject>(timerPS);
        upperParticle.transform.SetParent(upperTimer.transform);
        upperTimer.transform.SetParent(transform);

        downTimer = Instantiate<GameObject>(timerCylinderPrefab);
        downParticle = Instantiate<GameObject>(timerPS);
        downParticle.transform.SetParent(downTimer.transform);
        downTimer.transform.SetParent(transform);

        setTimers(1);

    }


    public void setTimers(float r)
    {

        upperTimer.transform.SetParent(null);
        downTimer.transform.SetParent(null);


        r = Mathf.Clamp(r, 0, 1);

        var bounds = GetComponent<Renderer>().bounds;

        Vector3 end = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        Vector3 start = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);

        start = Vector3.Lerp(start, end, 1-r);

        upperTimer.transform.position = (start + end) / 2;
        upperTimer.transform.localScale = new Vector3(1, Vector3.Distance(end, start) / 2, 1);
        upperTimer.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);

        upperParticle.transform.position = start;
        

        end.z = bounds.min.z;
        start.z = bounds.min.z;

        
        downTimer.transform.position = (start + end) / 2;
        downTimer.transform.localScale = new Vector3(1, Vector3.Distance(end, start) / 2, 1);
        downTimer.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);


        downParticle.transform.position = start;

        upperTimer.transform.SetParent(transform);
        downTimer.transform.SetParent(transform);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
