using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colission : MonoBehaviour
{   
    private Rigidbody rb;

    private void Awake(){
        //Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.transform.tag == "Aeronave"){
            Destroy(collision.gameObject);
        }
    }
}