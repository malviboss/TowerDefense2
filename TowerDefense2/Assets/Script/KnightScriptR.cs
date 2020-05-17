using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScriptR : MonoBehaviour
{


    // Start is called before the first frame update
    Animator player_anim;
    
    AnimatorClipInfo[] m_CurrentClipInfo;

    float m_CurrentClipLength;
    
    public bool isAttacking, isDead;
    public float moveSpeed = 0f;
    public float lives = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player_anim= gameObject.GetComponent<Animator>();
        m_CurrentClipInfo=player_anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        Debug.Log(m_CurrentClipInfo);
        Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //controlla se sono rimaste vite
        if(lives <= 0){
            //cambia parametro in animator per attivare animazione di morte
            isDead =true; //al prossimo frame aggiornerà animazione
            Debug.Log("Morto");
        }
        

        if (Input.GetMouseButtonDown(1))
        {
            //attacca
            Debug.Log("Premuto tasto destro mouse");
            isAttacking = true;

        }else{
            //Debug.Log("Non ho premuto A ma un altro tasto");
            isAttacking = false; 
        }
        
        //Controlla se sta attaccando o no
        if (isAttacking == false)
            player_anim.SetBool("isAttacking", false);

        if (isAttacking == true)
            player_anim.SetBool("isAttacking", true);
        
        if (isDead == true){
            player_anim.SetBool("isDead", true);
            //distrugge l'oggetto dopo aver aspettato il numero di secondi
            //necessari a mostrare l'animazione
            Destroy(gameObject,m_CurrentClipLength);
        }

        //prove varie di debug
        if (Input.GetKeyDown(KeyCode.G))
        {
            lives = lives -1;
            Debug.Log("Vite scese a "+ lives+"");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }


    void OnTriggerEnter2D (Collider2D col){
         ScoreScript.scoreValue +=1;
         Debug.Log("hit Detection");
    }
}
