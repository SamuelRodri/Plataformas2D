using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public void UpdateScore(int score)
        => scoreText.text = $"Score: {score}";
}
