using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giro : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,60,0)*Time.deltaTime);
    }   
}
