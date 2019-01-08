using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    public CubeController cubeWorld;
    public PlayerController player;
    public CameraController cam;
    public GameObject ascentObject;
    public GameObject descentObject;
    public GameController gameController;
    public Collider reverseObstacle;

    MeshRenderer rend;
    Collider coll;

    public int wallPower = 0;
    

	// Use this for initializationri
	void Start () {
        rend = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject==player.gameObject)
        {
            //cam.transmissionMode = true;
            //StartCoroutine(player.ascentDescent(ascentObject, descentObject));
            //StartCoroutine(cubeWorld.setSeasonRotation(transform.localRotation.eulerAngles + new Vector3(0, 0, 90)));
            gameController.nextSesion();
            StartCoroutine(nextSessiong());
        }

    }


    public void breakWall()
    {
        rend.enabled =false;
        coll.isTrigger = true;
    }

    public void buildWall(int numberOfKey)
    {
        
        if(rend==null)
            rend = GetComponent<MeshRenderer>();

        if(coll==null)
            coll = GetComponent<Collider>();


        rend.enabled = true;
        coll.isTrigger =false;

        gameController.uiController.setKey(numberOfKey);
        wallPower = numberOfKey;
    }

    public void damageWall()
    {
        wallPower-=1;
        if (wallPower <= 0)
            breakWall();

        gameController.uiController.setKey(wallPower);
    }

    IEnumerator nextSessiong()
    {

        reverseObstacle.isTrigger = true;

        this.enabled = false;
        //Enable cameras transmission mode
        if(cam!=null)
            cam.transmissionMode = true;
        //StartCoroutine(cam.lerpToFarCenter());


        //Ascent player for transition
        //Set parent of player as cubewolrd hile cube world is gonna be rotated too
        
        player.enabled = false;
        //yield return StartCoroutine(player.Tween(ascentObject, player.ascendSpeed));
        yield return StartCoroutine(player.lerpPos(new Vector3(ascentObject.transform.position.x,ascentObject.transform.position.y,player.transform.position.z), player.ascendSpeed));

        

        player.transform.SetParent(ascentObject.transform);

        cubeWorld.enabled = false;
        var delta = cubeWorld.transform.rotation.eulerAngles.z % 90;

        StartCoroutine(cubeWorld.lerpRotation(Quaternion.Euler(cubeWorld.transform.localRotation.eulerAngles+new Vector3(0, 0, 90-delta)), 1f));
        yield return StartCoroutine(player.lerpRotation(Quaternion.Euler(0,90,0), 1f));
        cubeWorld.enabled = true;
   
        player.transform.SetParent(cubeWorld.transform);

        if(cam!=null)
            cam.transmissionMode = false;
        //StartCoroutine(cam.lerpToPalyer());
        //yield return StartCoroutine(player.Tween(descentObject, player.descendSpeed));
        yield return StartCoroutine(player.lerpPos(new Vector3(descentObject.transform.position.x, descentObject.transform.position.y, player.transform.position.z), player.ascendSpeed));
        player.enabled = true;


        reverseObstacle.isTrigger = false;
        yield break;
    }



}
