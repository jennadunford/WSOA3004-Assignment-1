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
    [SerializeField] Text ingameHighScore;

    const string scoreMsg = "Score: ";
    const string highScoreMsg = "High: ";

    const string scoreSave = "score";


    [SerializeField] float secPerInc;
    float incCount = 0f;
    bool running = false;
    public float Score { get; private set; } = 0f;
    private void Start()
    {
        losePanel.SetActive(false);
        ingameHighScore.text = highScoreMsg + GetHighScore();
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
        running = false;

        SetHighSocre(Score);
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


    // High score managment
    private float GetHighScore()
    {
        if (PlayerPrefs.HasKey(scoreSave))
        {
            return PlayerPrefs.GetFloat(scoreSave);
        }
        else
        {
            return 0f;
        }
    }
    private void SetHighSocre(float score)
    {
        if(score > GetHighScore())
        {
            PlayerPrefs.SetFloat(scoreSave, score);
        }
    }
}
