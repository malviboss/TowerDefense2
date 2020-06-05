using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.UI;
public class MenuPrincipale : MonoBehaviour
{
    public Text highScoreText;

    public Slider VolumeSlider;
    
    public GameObject gameOverText,restartButton;

    float volume;
    void Start()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
        volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        VolumeSlider.value = volume;
    }
    public void PlayGame()
    {
                gameOverText.SetActive(false);
        restartButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
public void GoToHighscoresTable()
    {
        SceneManager.LoadScene("ScoreTable");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    

}