using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempLoseUI : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    [SerializeField] Text loseMsg;
    [SerializeField] Text scoreTxt;

    [SerializeField] Text ingameScore;

    const string scoreMsg = "Score: ";


    [SerializeField] float secPerInc;
    float incCount = 0f;
    bool running = false;
    public float Score { get; private set; } = 0f;
    private void Start()
    {
        losePanel.SetActive(false);
    }
    void Update()
    {
        if (running)
        {
            incCount += Time.deltaTime;
            if (incCount >= secPerInc)
            {
                incCount = 0;
                Score++;
            }
        }

        ingameScore.text = scoreMsg + Score.ToString("0");
    }

    // Events called by event manager
    public void StartRunning()
    {
        running = true;
    }

    public void Lose(string msg)
    {
        loseMsg.text = msg;
        scoreTxt.text = scoreMsg + Score.ToString("0");
        losePanel.SetActive(true);
    }


    // Events called by UI 
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
