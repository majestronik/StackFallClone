using UnityEngine;

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

    [SerializeField]
    AudioClip win, death, idestroy, destroy, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;

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
                print("invincible");
                currentTime -= Time.deltaTime * 0.35f;
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
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
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
        if (playerState == PlayerState.Prepare)
        {
            if (Input.GetMouseButton(0))
            {
                playerState = PlayerState.Playing;
            }
        }
        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButton(0))
            {
                FindObjectOfType<LevelSpawner>().nextLevel();
            }
        }

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
        Debug.Log("currentObstacleNumber = " + currentObstacleNumber);
        Debug.Log("totalObstacleNumber = " + totalObstacleNumber);

        FindObjectOfType<GameUI>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);

        if (collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SoundManager.instance.playSoundFX(win, 0.5f);
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
