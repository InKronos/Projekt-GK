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
    public GameObject[] enemiesPrefabs;
    public List<GameObject> enemies = new List<GameObject>();
    int level = 1;
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

    public void Update()
    {
        checkLevel();
    }

    private void checkLevel()
    {
        if(enemies.Count.Equals(0))
        {
            level++;
            SpawnEnemies();
        }
    }
    private void SpawnEnemies()
    {
        for(int i = 0; i < level; i++)
        {
            enemies.Add(Instantiate(enemiesPrefabs[0], new Vector3(600, 33, 347), Quaternion.identity));
        }
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
