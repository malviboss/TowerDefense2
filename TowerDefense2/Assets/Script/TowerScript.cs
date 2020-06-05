using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    Animator towerAnim;
    public static bool isDead;
    AnimatorClipInfo[] m_CurrentClipInfo;
    Rigidbody2D rb;
    public GameObject heart1T, heart2T, heart3T, gameOverText, restartButton;
    public static int lives = 3;
    float m_CurrentClipLength;
 public float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {        

        heart1T = GameObject.Find("heart1T");
        heart2T = GameObject.Find("heart2T");
        heart3T = GameObject.Find("heart3T");
        gameOverText = GameObject.Find("GameOverText");
        restartButton = GameObject.Find("RestartButton");
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        heart1T.gameObject.SetActive(true);
        heart2T.gameObject.SetActive(true);
        heart3T.gameObject.SetActive(true);
        towerAnim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = towerAnim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;

                isDead = false;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
                //prove varie di debug
        if (Input.GetKeyDown(KeyCode.G))
        {
            lives = lives - 1;

            Debug.Log("Vite scese a " + lives + "");
        }
                switch (lives)
        {
            case 2:
                heart3T.gameObject.SetActive(false);
                break;
            case 1:
                heart2T.gameObject.SetActive(false);
                break;
            case 0:
                heart1T.gameObject.SetActive(false);         //cambia parametro in animator per attivare animazione di morte
                isDead = true; //al prossimo frame aggiornerà animazione
                Debug.Log("Morto");
                break;
        }


        if (isDead == true)
        {

            towerAnim.SetBool("isDead", true);
            //distrugge l'oggetto dopo aver aspettato il numero di secondi
            //necessari a mostrare l'animazione
            Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            Time.timeScale = 0;  
        }
    }



  void OnCollisionEnter2D(Collision2D col)
    {

       // if (!col.gameObject.name.Equals("ground") && !col.gameObject.name.Equals("Knight2") && !col.gameObject.name.Equals("Tower"))
        if ((col.gameObject.name.Equals("GolemL(Clone)") || col.gameObject.name.Equals("GolemR(Clone)")))
        {
            Debug.Log(col.gameObject.name);


                lives = lives-1;
        }
    }
}
