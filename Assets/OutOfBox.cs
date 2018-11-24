using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBox : MonoBehaviour {

    public GameController gameController;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            gameController.lost();
    }
}
