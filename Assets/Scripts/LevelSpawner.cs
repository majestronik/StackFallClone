using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{

    public GameObject[] obstacleModels;

    [HideInInspector]
    public GameObject[] obstaclePrefabs = new GameObject[4];

    public GameObject winPrefab;

    private GameObject temp1Obstacle, temp2Obstacle;
    public int level = 1;
    private int addNumber = 7;
    int randomNumber;
    float obstacleNumber;


    private void Awake()
    {
        randomObstacleGenerator();

        level = PlayerPrefs.GetInt("Level", 1);
        float randomHardness = Random.value;
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f)
        {
            if (level <= 20) randomNumber = Random.Range(0, 2);
            if (level > 20 && level <= 50) randomNumber = Random.Range(1, 3);
            if (level > 50 && level <= 100) randomNumber = Random.Range(2, 4);

            temp1Obstacle = Instantiate(obstaclePrefabs[(randomNumber)]);
            temp1Obstacle.transform.parent = FindObjectOfType<RotateManager>().transform;
            temp1Obstacle.transform.position = new Vector3(0, obstacleNumber - temp1Obstacle.transform.localScale.y, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);

            if (Mathf.Abs(obstacleNumber) >= level * 0.3f && Mathf.Abs(obstacleNumber) <= level * 0.6f)
            {
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp1Obstacle.transform.eulerAngles += Vector3.up * 180;
            }
            else if (Mathf.Abs(obstacleNumber) >= level * 0.8f)
            {
                temp1Obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                if (randomHardness >= 0.75f)
                {
                    temp1Obstacle.transform.eulerAngles += Vector3.up * 180;
                }
            }
        }

        temp2Obstacle = Instantiate(winPrefab);
        temp2Obstacle.transform.position = new Vector3(0, temp1Obstacle.transform.position.y - 0.5f, 0);
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public void nextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene("MainScene");
    }

    public void randomObstacleGenerator()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + 4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + 8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + 12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + 16];
                }
                break;
            default:
                break;
        }
    }
}
