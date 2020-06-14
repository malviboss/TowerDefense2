using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public Text score;
    public Text highScore;

    void Start()
    {
        score = GetComponent<Text> ();
   


    }

    void Update()
    {
        
        score.text = "Score :" + scoreValue.ToString();
        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScore.text = scoreValue.ToString();
        }


            if (Input.GetKeyDown(KeyCode.Y))
        {
            scoreValue++;
            
            SuperShotScript.fillSupershotBar++;

        }
    }

    public int getNumber()
    {
        return scoreValue;
    }
}
