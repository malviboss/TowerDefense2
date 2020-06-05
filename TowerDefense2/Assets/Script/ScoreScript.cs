using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public Text score;
    public Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text> ();
   
        highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
        score.text = "Score :" + scoreValue.ToString();
        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScore.text = scoreValue.ToString();
        }
    }
}
