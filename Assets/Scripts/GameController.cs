using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    private static int score = 0;

    [SerializeField] private Player player;

    [Header("Door Configuration")]
    [SerializeField] private Transform[] doorPoints;
    [SerializeField] private Door doorPrefab;

    [Header("Key Configuration")]
    [SerializeField] private Transform[] keyPoints;
    [SerializeField] private Key keyPrefab;

    [Header("Others")]
    [SerializeField] private UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        uiController.UpdateScore(score);

        Transform actualDoorPoint = doorPoints[Random.Range(0, doorPoints.Length)];
        var door = Instantiate(doorPrefab, actualDoorPoint.position, Quaternion.identity);

        door.OnDoorOpen += RestartLevel;

        Transform actualKeyPoint = keyPoints[Random.Range(0, keyPoints.Length)];
        var key = Instantiate(keyPrefab, actualKeyPoint.position, Quaternion.identity);

        player.OnDie += GameOver;
    }

    private void RestartLevel()
    {
        score += 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        uiController.ShowGameOver(score);
    }
}
