using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ScoreUpdate score;
    bool GameHasEnded = false;
    public GameObject completeLevelUI;
    public void RestartGame(){
        if (GameHasEnded == false)
        {
            Invoke("Restart", 0.5f);
        }
    }
    public void EndGame()
    {
        GameHasEnded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        score.enabled = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
