using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreaker : MonoBehaviour
{

    public FinishLine finishLine;
    public AudioClip breakEffect;
    SoundController soundController;


    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().soundController;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            finishLine.damageWall();
            Destroy(gameObject);

            soundController.createSoundEffect(breakEffect);
            
        }
    }
}
