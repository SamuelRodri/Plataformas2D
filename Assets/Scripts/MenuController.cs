using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    public void GoToGame()
        => SceneManager.LoadScene("GameScene");

    public void GoToCredits()
    {
        menuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void ExitGame()
        => Application.Quit();

    public void GoToMenu()
    {
        menuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }
}
