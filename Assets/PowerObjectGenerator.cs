using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObjectGenerator : MonoBehaviour {



    public GameObject[] powerUpPrefabs;

    [Serializable]
    public struct StaticPowerDowns
    {
        public RandomProbeGenerator.sesions session;
        public GameObject prefab;
    }


    [Serializable]
    public struct DynamicPowerDowns
    {
        public RandomProbeGenerator.sesions session;
        public GameObject prefab;
    }

    public StaticPowerDowns[] staticPowerDowns;
    public DynamicPowerDowns[] dynamicPoweDowns;
    public GameObject wind;

    public GameController gameController;

    public float powerUpChance = 0.5f;
    public float windChance = 0.3f;
    public int maxPowerDownPerLevel = 10;
    public int minPowerDownPerLelvel = 5;

    private void Start()
    {
        gameController.GetComponent<GameController>();
    }

    public void spawnWind()
    {

        if (UnityEngine.Random.Range((int)0, (int)(1 / windChance)) == 0)
        {
            var reverseSesion = gameController.geReverseSession();
            var reverseSesionObject = gameController.probeGenerator.getSessionObject(reverseSesion);

            var spawnedObject = GameObject.Instantiate<GameObject>(wind);
            spawnedObject.transform.SetParent(reverseSesionObject.transform);
            var randomPos = gameController.probeGenerator.findRandomPosInBounds(reverseSesionObject.transform.parent.gameObject);
            spawnedObject.transform.rotation = reverseSesionObject.transform.rotation;
            spawnedObject.transform.position = randomPos;
            spawnedObject.transform.Rotate(spawnedObject.transform.up, UnityEngine.Random.Range(0, 360));
            spawnedObject.transform.position += spawnedObject.transform.up * 5;
        }
    }

    public void spawnPowerUp()
    {
        var res = UnityEngine.Random.Range((int)0, (int)(1 / powerUpChance));
        
        if (res == 0)
        {
            var reverseSesion = gameController.geReverseSession();
            var powerToSpawn = powerUpPrefabs[UnityEngine.Random.Range(0, powerUpPrefabs.Length)];
            var reverseSesionObject = gameController.probeGenerator.getSessionObject(reverseSesion);

            var spawnedObject = GameObject.Instantiate<GameObject>(powerToSpawn);
            spawnedObject.transform.SetParent(reverseSesionObject.transform);
            var randomPos = gameController.probeGenerator.findRandomPosInBounds(reverseSesionObject.transform.parent.gameObject);
            spawnedObject.transform.rotation = reverseSesionObject.transform.rotation;
            spawnedObject.transform.position = randomPos;
            spawnedObject.transform.position += spawnedObject.transform.up * 1;
            
        }
    }

    public void spawnPowerDowns()
    {
        var currentSesion = gameController.currentSesion;
        var currentSesionObject = gameController.probeGenerator.getSessionObject(currentSesion);

        GameObject objToSpawn = null;
        for (var ss = 0; ss < dynamicPoweDowns.Length; ss++)
        {
            if (dynamicPoweDowns[ss].session == currentSesion)
                objToSpawn = dynamicPoweDowns[ss].prefab;
        }
        var spawnedObject = GameObject.Instantiate<GameObject>(objToSpawn);

        

        var finalLine = currentSesionObject.transform.parent.GetComponentInChildren<FinishLine>();
        var spawnPos = finalLine.gameObject.transform.position;

        spawnedObject.transform.position = spawnPos;
        spawnedObject.transform.SetParent(currentSesionObject.transform);
        spawnedObject.transform.rotation = Quaternion.LookRotation(-1 * finalLine.transform.right, finalLine.transform.forward);

        spawnedObject.GetComponent<SlowerPowerDown>().gameController = gameController;

        print("spawned power object");

        return;

        //var numberOfPowerDown = UnityEngine.Random.Range(minPowerDownPerLelvel, maxPowerDownPerLevel);

        //var currentSesion = gameController.currentSesion;
        //var reverseSesion = gameController.geReverseSession();

        //var reverseSesionObject=gameController.probeGenerator.getSessionObject(reverseSesion);
        //var currentSesionObject = gameController.probeGenerator.getSessionObject(currentSesion);

        //for (var i = 0; i < numberOfPowerDown; i++)
        //{
        //    var isStatic = (UnityEngine.Random.Range((int)0, (int)2)==0);

        //    if (isStatic)
        //    {

        //        GameObject objToSpawn = null;
        //        for (var ss = 0;ss < staticPowerDowns.Length; ss++)
        //        {
        //            if (staticPowerDowns[ss].session == reverseSesion)
        //                objToSpawn = staticPowerDowns[ss].prefab;
        //        }
        //        var spawnedObject = GameObject.Instantiate<GameObject>(objToSpawn);
        //        spawnedObject.transform.SetParent(reverseSesionObject.transform);
        //        var randomPos = gameController.probeGenerator.findRandomPosInBounds(reverseSesionObject.transform.parent.gameObject);
        //        spawnedObject.transform.rotation = reverseSesionObject.transform.rotation;
        //        spawnedObject.transform.position = randomPos;

                
        //    }
        //    else
        //    {
        //        GameObject objToSpawn = null;
        //        for (var ss = 0; ss < dynamicPoweDowns.Length; ss++)
        //        {
        //            if (dynamicPoweDowns[ss].session == currentSesion)
        //                objToSpawn = dynamicPoweDowns[ss].prefab;
        //        }
        //        var spawnedObject = GameObject.Instantiate<GameObject>(objToSpawn);
                
        //        var finalLine = currentSesionObject.transform.parent.GetComponentInChildren<FinishLine>();
        //        var spawnPos = finalLine.gameObject.transform.position;

        //        spawnedObject.transform.position = spawnPos;
        //        spawnedObject.transform.SetParent(currentSesionObject.transform);
        //        spawnedObject.transform.rotation = Quaternion.LookRotation(-1*finalLine.transform.right,finalLine.transform.forward);


        //    }

          
        //}

    }
}
