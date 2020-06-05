using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemR : MonoBehaviour
{
      AnimatorClipInfo[] m_CurrentClipInfo;
    public float moveSpeed = 50f;
        public bool isAttacking, isDead;

    Animator anim;

    public float m_CurrentClipLength;






    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
      //  Debug.Log(m_CurrentClipInfo);
       // Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
    }

    // Update is called once per frame
        void Update()
    {
        if (isAttacking == true)
        {
            //cambia parametro del corrispondente Animator
            anim.SetBool("isAttacking", true);
            //fai cose per infliggere danno al player o alla torre
        }
        //muoviti(c'è un modo migliore per farlo senza sdoppiare script(?)... ma per ora va bene)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);


        if (isDead == true)
        {
             moveSpeed = 0f;
            anim.SetBool("isDead", true);
            //distrugge l'oggetto dopo aver aspettato il numero di secondi
            //necessari a mostrare l'animazione
            Destroy(gameObject, m_CurrentClipLength);
        }

             if (Input.GetKeyDown(KeyCode.A))
        {
            isAttacking = false;
            isDead = true;

        }
    }

 void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Tower") {
            Debug.Log ("Hit Tower");
            //fermati
            moveSpeed = 0f;
            isAttacking = true;
            
        }
                if ((col.gameObject.name.Equals("Knight1")) || (col.gameObject.name.Equals("Knight2")))
        {
            //quando colpisci il giocatore
        //    Debug.Log("Hit player");
            //fermati
            moveSpeed = 0f;
            //fai cose per avviare animazione di attacco
            isAttacking = true;
           
            //quando player muore deve ricominciare a muoversi
            //settando isAttacking a false
            if (KnightScriptR.isAttacking != true && isAttacking==true)
            {
                
                KnightScriptR.lives = KnightScriptR.lives - 1; 
                if (KnightScriptR.lives == 0){
                    isAttacking = false;
                    moveSpeed = 50f;
                }else {
                    KnightScriptR.isHurt = true;
                }
            }
            
            
        }
    }
    

}
