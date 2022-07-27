using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] points;
    private static WayPoints _instance;

    void Awake()
    {
        _instance = this;
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public static WayPoints Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }

    }

    public Transform[] returnPoints()
    {
        return points;
    }
}
