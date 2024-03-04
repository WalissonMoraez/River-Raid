using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vzriv : MonoBehaviour
{
    private float contador_tempo=0;
    void Start(){

    }

    void Update(){
        contador_tempo+=Time.deltaTime;
        if( contador_tempo>=2f){
            Destroy(gameObject);
        }
    }

}
