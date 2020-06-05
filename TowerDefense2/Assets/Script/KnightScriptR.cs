using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScriptR : MonoBehaviour
{


    // Start is called before the first frame update
    Animator player_anim;

    AnimatorClipInfo[] m_CurrentClipInfo;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public Selected sel;
    float m_CurrentClipLength;
    bool isGrounded;
    public static bool isAttacking, isDead,  isActive, isJumping, isHurt;
    public bool isMoving;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float moveSpeed = 0f;
    public GameObject heart1R, heart2R, heart3R;
    public static int lives = 3;
    public float jump;
    int playerLayer, enemyLayer;
   // bool coroutineAllowed = true;
    Color color;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        heart1R = GameObject.Find("heart1R");
        heart2R = GameObject.Find("heart2R");
        heart3R = GameObject.Find("heart3R");
        heart1R.gameObject.SetActive(true);
        heart2R.gameObject.SetActive(true);
        heart3R.gameObject.SetActive(true);


        sel = gameObject.GetComponent<Selected>();
        player_anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = player_anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        // Debug.Log(m_CurrentClipInfo);
        //  Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
        isMoving = false;
         isGrounded = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (lives)
        {
            case 2:
                heart3R.gameObject.SetActive(false);
               
                break;
            case 1:
                heart2R.gameObject.SetActive(false);
              
                break;
            case 0:
                heart1R.gameObject.SetActive(false);         //cambia parametro in animator per attivare animazione di morte
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
            CheckIfGrounded();
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
        }
        else
        {
            //solo per testare, si può mettere che scompare un indicatore
            //fare cose
            //solo per testare se si esegue
            //si esegue quindi isActive si mette a false
            GetComponent<Renderer>().material.color = Color.blue;
            moveSpeed = 0f;
            jump = 0f;

        }

        if (isDead == true)
        {
            sel.isActive = false;
            player_anim.SetBool("isDead", true);
            //distrugge l'oggetto dopo aver aspettato il numero di secondi
            //necessari a mostrare l'animazione
            Destroy(gameObject, m_CurrentClipLength);
        }
        if (isHurt){
            player_anim.SetBool("isHurt", true);
            isHurt = false;
        } else {
            player_anim.SetBool("isHurt", false);
        }
        //prove varie di debug
        if (Input.GetKeyDown(KeyCode.P))
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
        }
    }



void Jump() {
 if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
 jump = 70f;
 rb.velocity = new Vector2(rb.velocity.x,jump);
 //metto isGrounded a false così non posso saltare
 //mentre è in aria
 isGrounded = false;
 }
 //funzionare funziona, ma da rifinire...
 }

    void CheckIfGrounded()
    {
       
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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
                Destroy(col.gameObject, 2f);
                
                ScoreScript.scoreValue++;
            }
            else if (col.gameObject.name.Equals("GolemR(Clone)"))
            {
                Debug.Log ("MORTO GOLEM R");
                Destroy(col.gameObject, 2f);
                ScoreScript.scoreValue++;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
                 if(col.gameObject.name == "ground"){
 //ho toccato il terreno, posso saltare di nuovo
 isGrounded = true;
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
