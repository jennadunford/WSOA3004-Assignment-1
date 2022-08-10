using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public Text timerText;
    public Text highScoreText;
    public static int scoreCounter = 0;
    public static int highScoreCount = 0;
    public static float rawTime = 0;

    public static bool gameEnd = false;

    public GameObject endPanel;
   
    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            scoreCounter += (int)(Mathf.Ceil(Time.deltaTime));
            timerText.text = scoreCounter.ToString();
            highScoreText.text = highScoreCount.ToString();
        }
        if(gameEnd)
        {
            endPanel.SetActive(true);
        }
    }
}
