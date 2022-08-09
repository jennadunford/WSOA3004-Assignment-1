using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(1);
    } 

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LosingScreen()
    {
        SceneManager.LoadScene(3);
    }
}
