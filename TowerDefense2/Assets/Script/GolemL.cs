using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemL : MonoBehaviour
{
    float moveSpeed = 50f;
    public bool isAttacking = false;

    Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

      if(isAttacking == true) {
            //cambia parametro del corrispondente Animator
            anim.SetBool("isAttacking",true);
            //fai cose per infliggere danno al player o alla torre
        }
        //muoviti(c'è un modo migliore per farlo senza sdoppiare script(?)... ma per ora va bene)
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

 void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Eseguito OnCollisionEnter");
        if (col.gameObject.name == "Tower") {
            Debug.Log ("Hit Tower");
            //fermati
            moveSpeed = 0f;
            isAttacking = true;
            
        }
        if((col.gameObject.name == "Knight1") || (col.gameObject.name == "Knight2")){
            //quando colpisci il giocatore
            Debug.Log("Hit player");
            //fermati
            moveSpeed = 0f;
            //fai cose per avviare animazione di attacco
            isAttacking = true;
            //manda messaggio a gameobject player di prendere danno
            //quando player muore deve ricominciare a muoversi
            //settando isAttacking a false
        }
    }
    

}
