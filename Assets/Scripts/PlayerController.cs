using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Prepare,
    Playing,
    Died,
    Finish
}

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool isTouch;
    float currentTime;
    bool invincible = false;
    public ParticleSystem fireEffect;
    public bool finishAnim = false;

    [SerializeField]
    AudioClip win, death, idestroy, destroy, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;
    public Image invincibleImage;
    public CameraController _cameraController;
    private bool invincibleIncreasing;

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
        fireEffect.gameObject.SetActive(false);
    }

    void Start()
    {
        totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
    }
    void Update()
    {
        setInvincibleRadialBar(currentTime);
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouch = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;
            }

            if (invincible)
            {
                if (!fireEffect.gameObject.activeInHierarchy)
                {
                    fireEffect.gameObject.SetActive(true);
                }
                currentTime -= Time.deltaTime * 0.35f;
                invincibleIncreasing = false;
            }
            else
            {
                if (fireEffect.gameObject.activeInHierarchy)
                {
                    fireEffect.gameObject.SetActive(false);
                }
                if (isTouch)
                {
                    currentTime += Time.deltaTime * 0.8f;
                    invincibleIncreasing = true;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                    invincibleIncreasing = false;
                }
            }

            if (currentTime >= 1f)
            {
                currentTime = 1f;
                invincible = true;
            }
            else if (currentTime <= 0f)
            {
                currentTime = 0;
                invincible = false;
            }
        }
        if (playerState == PlayerState.Finish)
        {
            {
                FindObjectOfType<LevelSpawner>().nextLevel();
            }
        }

    }

    public void setInvincibleRadialBar(float value)
    {
        if (invincibleIncreasing) invincibleImage.color = Color.green;
        else invincibleImage.color = Color.yellow;
        invincibleImage.fillAmount = value;
    }
    public void getScore()
    {
        if (invincible)
        {
            ScoreManager.instance.addScore(1);
            return;
        }
        ScoreManager.instance.addScore(2);
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            if (isTouch)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTouch)
        {
            rb.velocity = new Vector3(0, 50 * Time.fixedDeltaTime * 5, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    SoundManager.instance.playSoundFX(idestroy, 0.5f);
                    getScore();
                    currentObstacleNumber++;
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    SoundManager.instance.playSoundFX(destroy, 0.5f);
                    getScore();
                    currentObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane")
                {
                    SoundManager.instance.playSoundFX(death, 0.5f);
                    ScoreManager.instance.ResetScore();
                    Debug.Log("You died.");
                }
            }
        }
        // Debug.Log("currentObstacleNumber = " + currentObstacleNumber);
        // Debug.Log("totalObstacleNumber = " + totalObstacleNumber);

        FindObjectOfType<GameUIController>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);

        if (collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {

            playerState = PlayerState.Finish;
            SoundManager.instance.playSoundFX(win, 0.5f);
            StartCoroutine(_cameraController.finishCam());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isTouch || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.fixedDeltaTime * 5, 0);
            SoundManager.instance.playSoundFX(bounce, 0.5f);
        }
    }
}
