using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScriptL : MonoBehaviour
{


    // Start is called before the first frame update
    Animator player_anim;

    AnimatorClipInfo[] m_CurrentClipInfo;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Selected sel;
    float m_CurrentClipLength;
    bool isGrounded;
    public static bool isAttacking, isDead,  isActive, isJumping, isHurt;
    public bool isMoving;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float moveSpeed;
    public GameObject heart1L, heart2L, heart3L;
    public static int lives = 3;
    public float jump;
    int playerLayer, enemyLayer;
    float lowJumpMultiplier = 40f;
    float fallMultiplier = 50f;
    //bool coroutineAllowed = true;
    Color color;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        heart1L = GameObject.Find("heart1L");
        heart2L = GameObject.Find("heart2L");
        heart3L = GameObject.Find("heart3L");
        heart1L.gameObject.SetActive(true);
        heart2L.gameObject.SetActive(true);
        heart3L.gameObject.SetActive(true);

     isGrounded = true;
        sel = gameObject.GetComponent<Selected>();
        player_anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = player_anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        // Debug.Log(m_CurrentClipInfo);
        //  Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
        isMoving = false;
        isJumping = false;
        isHurt = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (lives)
        {
            case 2:
                heart3L.gameObject.SetActive(false);
                isHurt = false;
                break;
            case 1:
                heart2L.gameObject.SetActive(false);   
                isHurt = false;       
                break;
            case 0:
                heart1L.gameObject.SetActive(false);     
                isHurt = false;    //cambia parametro in animator per attivare animazione di morte
                isDead = true; //al prossimo frame aggiornerà animazione
                
                Debug.Log("Morto");
                sel.isDead = true;
                break;    
        }
        isActive = sel.isActive;
        //queste azioni avvengono solo se isActive= true
        if (isActive == true)
        {
            //controllo cambio giocatore

            //solo per testare
            GetComponent<Renderer>().material.color = Color.red;
            //movimento
            Move();
            //salto;
            JumpPolish();
            Jump();
            //attacco
            Attack();
            //Controlla se si sta muovendo o no
            if (isMoving == true)
                player_anim.SetBool("isMoving", true);
            else
                player_anim.SetBool("isMoving", false);

            //Controlla se sta attaccando o no
            if (isAttacking == true)
                player_anim.SetBool("isAttacking", true);
            else
                player_anim.SetBool("isAttacking", false);
            if (isJumping == true)
                player_anim.SetBool("isJumping", true);
            else
                player_anim.SetBool("isJumping", false);
        }
        else
        {
            //solo per testare, si può mettere che scompare un indicatore
            //fare cose
            //solo per testare se si esegue
            //si esegue quindi isActive si mette a false
            GetComponent<Renderer>().material.color = Color.blue;
            JumpPolish();
            rb.velocity = new Vector2(0, rb.velocity.y);
            player_anim.SetBool("isMoving", false);
            player_anim.SetBool("isJumping", false);
            
            isMoving = false;

        }

        if (isDead == true)
        {
            isHurt = false;
            sel.isActive = false;
            player_anim.SetBool("isDead", true);
            //distrugge l'oggetto dopo aver aspettato il numero di secondi
            //necessari a mostrare l'animazione
            Destroy(gameObject, m_CurrentClipLength);
        }
        if (isHurt){
            player_anim.SetBool("isHurt", true);
        } else {
            player_anim.SetBool("isHurt", false);
        }

        //prove varie di debug
        if (Input.GetKeyDown(KeyCode.O))
        {
            lives = lives - 1;

            Debug.Log("Vite scese a " + lives + "");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ScoreScript.scoreValue++;
        }
    }

    void Move()
    {
        moveSpeed = 40f;
        //pone x pari al valore dell'asse orizzontale
        //non premo nulla, x sarà 0 e non mi muovo
        //premo D o freccia destra, x sarà 1 e mi muovo lungo l'asse x a destra
        //premo A o freccia sinistra, x sarà -1 e mi muovo lungo l'asse x a sinistra
        float x = Input.GetAxisRaw("Horizontal");
        float move = x * moveSpeed;
        //muovi a destra o sinistra a seconda dell'input preso
        rb.velocity = new Vector2(move, rb.velocity.y);
        //fai cose varie per mostrare animazioni di camminata
        //e girare lo sprite
        if ((x != 0))
        {
            isMoving = true;
            if (x < 0)
            {
                //se maggiore di 0 mi sto muovendo verso destra e devo girare lo sprite
                spriteRenderer.flipX = false;
            }
            else
            {
                //mi sto muovendo verso sinistra
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            isMoving = false;
             rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void JumpPolish()
    {
        if (rb.velocity.y < 0)
        {
            //se ho velocità y negativa vuol dire mi trovo nella fase di discesa del salto
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            //mi trovo nella fase di salita del salto
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
           
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("ENTRA IN SALTO");
            jump = 150;
            rb.velocity = new Vector2(rb.velocity.x, jump);
           // rb.velocity = new Vector2(rb.velocity.x, -jump);
            //metto isGrounded a false così non posso saltare
            //mentre è in aria
            isGrounded = false;
            isJumping = true;
        }
        //funzionare funziona, ma da rifinire...
    }
 


    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //attacca
            Debug.Log("Premuto tasto mouse sinistro");
            isAttacking = true;

        }
        else
        {
            if (Time.time > m_CurrentClipLength)
                //Debug.Log("Non ho premuto A ma un altro tasto");
                isAttacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // if (!col.gameObject.name.Equals("ground") && !col.gameObject.name.Equals("Knight2") && !col.gameObject.name.Equals("Tower"))
        if (col.gameObject.name.Equals("GolemL(Clone)") || col.gameObject.name.Equals("GolemR(Clone)"))
        {
            Debug.Log(col.gameObject.name);
            //ScoreScript.scoreValue += 1;
            Debug.Log("hit Detection GOLEM L ");
            if (isAttacking == true)
                Debug.Log("IS ATTACKING?!");
            if (col.gameObject.name.Equals("GolemL(Clone)"))
            {
            Debug.Log ("MORTO GOLEM L");
                Destroy(col.gameObject);
                
                ScoreScript.scoreValue++;
            }
            else if (col.gameObject.name.Equals("GolemR(Clone)"))
            {
                Debug.Log ("MORTO GOLEM R");
                Destroy(col.gameObject);
                ScoreScript.scoreValue++;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.name == "ground") || (col.gameObject.name == "Tower"))
        {
            //ho toccato il terreno, posso saltare di nuovo
            isGrounded = true;
            isJumping = false;
        }

        /*  if (col.gameObject.name == "Tower")
          {
              Debug.Log("Hit Tower");
              //fermati
              moveSpeed = 0f;

          }

          if (col.gameObject.name == "GolemL")
          {
              Debug.Log("Hit GolemL");
              //fermati
              moveSpeed = 0f;
              isAttacking = true;

          }*/

    }



}
