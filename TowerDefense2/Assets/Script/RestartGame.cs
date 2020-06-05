using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public GameObject gameOverText, restartButton;
    // Start is called before the first frame update
    void Start()
    {
         gameOverText = GameObject.Find("GameOverText");
        restartButton = GameObject.Find("RestartButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartScene()
    {
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");




    }
}
