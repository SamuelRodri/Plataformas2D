using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
        Instantiate(doorPrefab, actualDoorPoint.position, Quaternion.identity);

        Transform actualKeyPoint = keyPoints[Random.Range(0, keyPoints.Length)];
        Instantiate(keyPrefab, actualKeyPoint.position, Quaternion.identity);
    }
}
