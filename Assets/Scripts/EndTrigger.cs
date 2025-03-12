using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameManager gameManager;
    public ScoreUpdate score;
    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
        score.StopRacing();
    }   
}