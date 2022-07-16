using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Image levelSlider;
    public Image currentLeveLImage;
    public Image nextLevelImage;
    public MeshRenderer playerMeshRenderer;
    private Material _playerMat;
    void Start()
    {
        _playerMat = playerMeshRenderer.material;

        levelSlider.color = _playerMat.color + Color.gray;

        currentLeveLImage.color = _playerMat.color;

        nextLevelImage.color = _playerMat.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void ChangeScoreText(int score)
    {

    }
}
