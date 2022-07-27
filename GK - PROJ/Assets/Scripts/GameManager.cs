using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [HideInInspector]
    public float minx = 148, maxx = 806;
    public float minz = 177, maxz = 786;
    public int points = 0;
    public TextMeshProUGUI scoreText;
    public GameObject turretPrefab;
    public GameObject tankPrefab;
    public GameObject[] asteroidPrefabs;
    public List<GameObject> enemies = new List<GameObject>();
    int level = 1;
    float z, x;
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
            float z = Random.Range(minz, maxz);
            float x = Random.Range(minx, maxx);
            enemies.Add(Instantiate(turretPrefab, new Vector3(x, -2.2f, z), Quaternion.identity));
        }
        
        for(int i = 0; i < level; i++)
        {
            float z = Random.Range(minz, maxz);
            float x = Random.Range(minx, maxx);
            float y = Random.Range(50, 500);
            enemies.Add(Instantiate(asteroidPrefabs[Random.Range(0, 2)], new Vector3(x, y, z), Quaternion.identity));
        }

        Transform waypoint = WayPoints.Instance.returnPoints()[0];
        enemies.Add(Instantiate(tankPrefab, new Vector3(waypoint.transform.position.z, 0, waypoint.transform.position.z), Quaternion.identity));
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
