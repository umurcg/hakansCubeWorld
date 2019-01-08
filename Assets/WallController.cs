    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wallBreakerPrefab;

    public GameObject summerWall;
    public GameObject winterWall;
    public GameObject springWall;
    public GameObject fallWall;

    public RandomProbeGenerator rpg;



    public void Start()
    {
        //createWall(RandomProbeGenerator.sesions.winter);
        //createWall(RandomProbeGenerator.sesions.summer);
        //createWall(RandomProbeGenerator.sesions.fall);
        //createWall(RandomProbeGenerator.sesions.spring);
    }

    public void createWall(RandomProbeGenerator.sesions sesion)
    {
        //GameObject wall=null;
        //GameObject sesionObject = null;

        //switch (sesion)
        //{
        //    case RandomProbeGenerator.sesions.fall:
        //        sesionObject = rpg.fall;
        //        wall = fallWall;
        //        break;
        //    case RandomProbeGenerator.sesions.winter:
        //        sesionObject = rpg.winter;
        //        wall = winterWall;
        //        break;
        //    case RandomProbeGenerator.sesions.summer:
        //        sesionObject = rpg.summer;
        //        wall = summerWall;
        //        break;
        //    case RandomProbeGenerator.sesions.spring:
        //        sesionObject = rpg.spring;
        //        wall = springWall;
        //        break;
        //}

        

        //var wallFL = wall.GetComponent<FinishLine>();
        //wallFL.buildWall(3);
        //wallFL.wallPower = 3;

        //for(int i = 0; i < 3; i++)
        //{
        //    var pos = rpg.findRandomPosInBounds(sesionObject.transform.parent.gameObject);
        //    var key = Instantiate<GameObject>(wallBreakerPrefab);
        //    key.transform.position = pos;
        //    key.transform.parent = sesionObject.transform;
        //    key.GetComponent<WallBreaker>().finishLine = wallFL;
            
        //}




    }

    
}
