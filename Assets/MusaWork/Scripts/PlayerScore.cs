using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Transform player;
    public Text scoreText;

    void Update()
    {
        scoreText.text = player.position.y.ToString("0");
    }
}
