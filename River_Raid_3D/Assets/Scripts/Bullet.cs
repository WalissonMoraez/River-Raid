using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject explosao_abstr;
    public GameObject explosao_fogo_abstr;
    public float life=1;
    void Awake(){
        Destroy(gameObject, life);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.transform.tag == "Mishen"){
            GameObject explosao = Instantiate(explosao_abstr) as GameObject;
            explosao.transform.position = transform.position;
            GameObject explosao_j= Instantiate(explosao_fogo_abstr) as GameObject;
            explosao_j.transform.position = transform.position;

        Destroy(collision.gameObject);
        Destroy(gameObject);
        }
    }

}
