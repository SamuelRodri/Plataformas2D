using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Door Configuration")]
    [SerializeField] private Transform[] doorPoints;
    [SerializeField] private Door doorPrefab;

    [Header("Key Configuration")]
    [SerializeField] private Transform[] keyPoints;
    [SerializeField] private Key keyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Transform actualDoorPoint = doorPoints[Random.Range(0, doorPoints.Length)];
        var door = Instantiate(doorPrefab, actualDoorPoint.position, Quaternion.identity);

        door.OnDoorOpen += RestartLevel;

        Transform actualKeyPoint = keyPoints[Random.Range(0, keyPoints.Length)];
        var key = Instantiate(keyPrefab, actualKeyPoint.position, Quaternion.identity);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
