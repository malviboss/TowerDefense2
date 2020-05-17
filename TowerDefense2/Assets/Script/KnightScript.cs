using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{

    void OnTriggerEnter2D (Collider2D col){
         ScoreScript.scoreValue +=1;
         Debug.Log("hit Detection");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
