using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProbeGenerator : MonoBehaviour {

    public enum sesions{
        spring,
        summer,
        fall,
        winter,
        COUNT
    }
    
    public GameObject summer;
    public GameObject[] summerProbes;

    public GameObject fall;
    public GameObject[] fallProbes;

    public GameObject winter;
    public GameObject[] winterProbes;

    public GameObject spring;
    public GameObject[] springProbes;

    public int minNumberOfProbeInGroup = 1;
    public int maxNumberOfProbeInGroup = 5;

    public int numberOfGroup = 5;
    public int maxNumberOfGroup = 30;

    public float minDistBetweenGroups = 5f;

    public float randomPosOffset = 0f;

    // Use this for initialization
    void Start () {

        //At start update all season probes
        
        updateSeasonProbes(sesions.summer);
        updateSeasonProbes(sesions.winter);
        updateSeasonProbes(sesions.fall);
        updateSeasonProbes(sesions.spring);


    }

    public void increaseNumberOfGroup()
    {
        if (numberOfGroup + 1 < maxNumberOfGroup)
            numberOfGroup += 1;
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateSeasonProbes(sesions sesion)
    {
        print(sesion + " is updated");

        GameObject sesionObject=null;
        GameObject[] sesionProbes = null;
 
        switch (sesion)
        {
            case (sesions.summer):
                sesionObject = summer;
                sesionProbes = summerProbes;
                break;
            case (sesions.winter):
                sesionObject = winter;
                sesionProbes = winterProbes;
                break;
            case (sesions.fall):
                sesionObject = fall;
                sesionProbes = fallProbes;
                break;
            case (sesions.spring):
                sesionObject = spring;
                sesionProbes = springProbes;
                break;
            default:
                break;
        }

        //First clear sesionObject children
        foreach (Transform child in sesionObject.transform) { 
            GameObject.Destroy(child.gameObject);
        }

        //Bounds sesionPlaneBounds = sesionObject.transform.parent.GetComponent<BoxCollider>().bounds;
        //var max = sesionPlaneBounds.max;
        //var min = sesionPlaneBounds.min;
        for(int i = 0; i < numberOfGroup; i++)
        {
            var groupCount = Random.Range(minNumberOfProbeInGroup, maxNumberOfProbeInGroup);
            GameObject[] groupObjects=new GameObject[groupCount];

            //Find group center origin
            var origin = findRandomPosInBounds(sesionObject.transform.parent.gameObject);
            
            for (int c = 0; c < groupCount; c++)
            {
                var randomObjectPrefab = sesionProbes[Random.Range(0, sesionProbes.Length)];
                var randomObject=Instantiate<GameObject>(randomObjectPrefab);

                
                //Get bound box of random object to set heught
                var objBound = randomObject.GetComponent<Collider>().bounds;
                var height = objBound.max.y - objBound.min.y;

                //Set rotation
                randomObject.transform.rotation = sesionObject.transform.rotation;
                //Rotate in y randomly
                randomObject.transform.localRotation = randomObject.transform.localRotation * Quaternion.Euler(0, Random.Range(0, 360), 0);

                //Set position of random object with setting its height
                randomObject.transform.position = findRandomPosInBounds(sesionObject.transform.parent.gameObject) + randomObject.transform.up * height / 2;

                randomObject.transform.SetParent(sesionObject.transform);

                groupObjects[c] = randomObject;
            }


            //fitObjectsToSmallestCircle(groupObjects, origin, sesionObject.transform.up);
        }

    }

    void fitObjectsToSmallestCircle(GameObject[] objects, Vector3 origin, Vector3 up)
    {
        Dictionary<GameObject, float> objToRadius = new Dictionary<GameObject, float>();

        var totalRadius = 0f;
        foreach(var obj in objects)
        {
            var sp=obj.AddComponent<SphereCollider>();
            var radius = sp.radius;

            totalRadius += radius;
            objToRadius[obj] = radius;               
            
        }

        

    }

    Vector3 findRandomPosInBounds(GameObject spawnParent)
    {
        //print(bound);

        //var x = Random.Range(bound.max.x-randomPosOffset, bound.min.x+ randomPosOffset);
        //var y = Random.Range(bound.max.y- randomPosOffset, bound.min.y+ randomPosOffset);
        //var z = Random.Range(bound.max.z- randomPosOffset, bound.min.z+ randomPosOffset);
        var rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithin = spawnParent.transform.TransformPoint(rndPosWithin * .5f);
        return rndPosWithin;
        //return new Vector3(x, y, z);
    }
    


}
