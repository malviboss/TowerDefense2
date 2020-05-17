using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    float moveSpeed = 50f;
    Rigidbody2D rb;
    bool facingRight = false;
    public static bool isAttacking = false;
    public static bool isWalking = true;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        /*
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        anim = GetComponent<Animator>();
        */
    }

    // Update is called once per frame
    void Update()
    {
       /* if (transform.position.x < -9f){
            dirX = 1f;
            
        } else if (transform.position.x>9f){
            dirX = -1f;
        }

        if(isWalking){
            anim.SetBool("isWalking",true);
        } else 
            anim.SetBool("isWalking",false);

        if (isAttacking){
            anim.SetBool("isAttacking",true);
        } else 
            anim.SetBool("isAttacking",false);

    */
        transform.Translate(Vector2.right * moveSpeed  * Time.deltaTime );
    }

    void FixedUpdate(){
      /*  if (isWalking){
            rb.velocity = new Vector2(dirX*moveSpeed,rb.velocity.y);
        } else {
            rb.velocity = Vector2.zero;
        }
        if (!isAttacking){
            rb.velocity = new Vector2(dirX*moveSpeed,rb.velocity.y);
        } else {
            rb.velocity = Vector2.zero;
        }
        */
    }
    
    V
}
