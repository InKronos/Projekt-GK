using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [HideInInspector]
    public int points = 0;
    public TextMeshProUGUI scoreText;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }

    }

    private void Awake()
    {
        _instance = this;
        UpdateScoretext();
    }

    private void UpdateScoretext()
    {
        scoreText.text = "SCORE: " + points.ToString();
    }

    public void ChangePoints(int changeAmount)
    {
        points += changeAmount;
        UpdateScoretext();
    }
}
