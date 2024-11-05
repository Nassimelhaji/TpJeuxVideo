using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        
        if (other.collider.tag == "Player"){
            GameObject.Find("Door").GetComponent<ExitDoor>().CanOpen=true;
            Destroy(gameObject);
            Debug.Log("la clé est touchée:::::::::::::::::::;"+ GameObject.Find("Door").GetComponent<ExitDoor>().CanOpen);
        }
    }
}

