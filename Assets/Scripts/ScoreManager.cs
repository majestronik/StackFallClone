using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    private TextMeshProUGUI _scoreText;

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
    private void Awake()
    {
        makeSingleton();
        Debug.Log(_scoreText);
        if (_scoreText == null)
        {
            _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            _scoreText.text = "0";
        }
    }

    public void addScore(int value)
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        _scoreText.text = score.ToString();
    }
    public void ResetScore()
    {
        score = 0;
    }
}
