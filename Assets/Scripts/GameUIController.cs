using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public Image levelSlider;
    public Image currentLeveLImage;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public Image nextLevelImage;
    public MeshRenderer playerMeshRenderer;
    private Material _playerMat;
    public GameObject settingBtn;
    public GameObject soundOnOffBtn;
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;
    public GameObject homeUI;
    public GameObject gameUI;
    private PlayerController _player;

    public bool buttonSettingsBO;
    void Start()
    {
        _playerMat = playerMeshRenderer.material;

        _player = FindObjectOfType<PlayerController>();

        levelSlider.color = _playerMat.color + Color.gray;

        currentLeveLImage.color = _playerMat.color;

        nextLevelImage.color = _playerMat.color;

        soundOnOffBtn.GetComponent<Button>().onClick.AddListener(call: () => print("hellooo"));

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ignoreUI() && _player.playerState == PlayerState.Prepare)
        {
            homeUI.SetActive(false);
            gameUI.SetActive(true);
            _player.playerState = PlayerState.Playing;
        }

        if (SoundManager.instance.sound)
        {
            soundOffBtn.SetActive(false);
            soundOnBtn.SetActive(true);
        }
        else
        {
            soundOffBtn.SetActive(true);
            soundOnBtn.SetActive(false);
        }
    }

    private bool ignoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResult);
        foreach (var item in raycastResult)
        {
            if (item.gameObject.GetComponent<IgnoreGameUI>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void openSettings()
    {
        buttonSettingsBO = !buttonSettingsBO;
        settingBtn.SetActive(buttonSettingsBO);
    }

}
