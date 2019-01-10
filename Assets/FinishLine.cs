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
    public GameObject doorObject;

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
        //rend.enabled =false;
        coll.isTrigger = true;

        if (doorObject != null)
            StartCoroutine(_fadeObjectOut(doorObject, 0.5f, true));
    }

    public void buildWall(int numberOfKey)
    {
        
        if(rend==null)
            rend = GetComponent<MeshRenderer>();

        if(coll==null)
            coll = GetComponent<Collider>();


        //rend.enabled = true;
        coll.isTrigger =false;

        gameController.uiController.setKey(numberOfKey);
        wallPower = numberOfKey;

        if (doorObject != null)
        {
            Renderer doorRend = doorObject.GetComponent<Renderer>();
            StandardShaderUtils.ChangeRenderMode(doorRend.material, StandardShaderUtils.BlendMode.Opaque);
            Color textureColor = doorRend.material.color;
            textureColor.a = 1;
            doorRend.material.color = textureColor;
        }

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

    

    public static IEnumerator _fadeObjectOut(GameObject obj, float speed, bool fullFade = false)
    {
        Renderer rend = obj.GetComponent<Renderer>();

        if (!rend) rend = obj.GetComponentInChildren<Renderer>();

        //Debug.Log(rend.material.name);

        Color textureColor = rend.material.color;
        float a = textureColor.a;

        //If object is already faded
        if (a == 0) yield break;

        //It is for changing rendered mode at right time
        bool willBeTransparent = true;
        StandardShaderUtils.BlendMode mode = (fullFade) ? StandardShaderUtils.BlendMode.Fade : StandardShaderUtils.BlendMode.Transparent;

        if (willBeTransparent)
        {
            StandardShaderUtils.ChangeRenderMode(rend.material, mode);
        }


        if (a == 1)
        {
            while (a > 0)
            {
                a -= Time.deltaTime * speed;
                textureColor.a = a;
                rend.material.color = textureColor;
                yield return null;
            }
            textureColor.a = 0;
            rend.material.color = textureColor;

        }



        if (!willBeTransparent)
        {
            StandardShaderUtils.ChangeRenderMode(rend.material, StandardShaderUtils.BlendMode.Opaque);
        }

        yield break;
    }
}


 
 
 public static class StandardShaderUtils
{
    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }

    }
}