using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    public List<Obstacle> obstacles = new List<Obstacle>();
    private GameObject[] childObjects;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            obstacles.Add(child.GetComponent<Obstacle>());
        }
    }
    public void ShatterAllObstacles()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Shatter();
        }
        StartCoroutine(RemoveAllShatters());
    }

    IEnumerator RemoveAllShatters()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
