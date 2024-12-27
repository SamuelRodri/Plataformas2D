using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private GameObject key;

    public void UpdateScore(int score)
        => scoreText.text = $"Score: {score}";

    public void ShowGameOver(int score)
    {
        highScore.text = $"High Score: {score}";
        gameOverPanel.SetActive(true);
    }

    public void ShowKeyUI()
        => key.SetActive(true);
}
