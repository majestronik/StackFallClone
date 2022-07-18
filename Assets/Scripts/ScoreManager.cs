using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public GameUIController gameUIController;
    public int score = 0;

    private void Awake()
    {
        makeSingleton();
        if (gameUIController.scoreText == null)
        {
            gameUIController.scoreText.text = "0";
        }
        else
        {
            gameUIController.scoreText.text = score.ToString();
        }
    }
    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            GameObject obj = new GameObject();
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void addScore(int value)
    {
        gameUIController.scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        gameUIController.scoreText.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
    }
}
