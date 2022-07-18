using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public int rotationSpeed;
    public Transform cameraRotater;

    private PlayerController _playerController;
    private Vector3 cameraPos;
    private Transform win;
    private float cameraOffset = 4f;

    void Awake()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
            cameraRotater.position = win.transform.position;
        }
        if (transform.position.y > player.position.y - 3 && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y + 3, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, transform.position.z);
        }

    }

    public IEnumerator finishCam()
    {
        transform.parent = cameraRotater;
        float delta = transform.rotation.eulerAngles.y;
        while (true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(1, 3f, -3f), Time.deltaTime);

            if (Mathf.Abs(Vector3.Distance(transform.localPosition, new Vector3(1, 3f, -3f))) < .05f)
            {
                break;
            }

            yield return new WaitForSeconds(0.016f);
        }
        while (true)
        {
            cameraRotater.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            Vector3 targetPostition = new Vector3(player.position.x,
                                        this.transform.position.y - 3,
                                        player.position.z);
            this.transform.LookAt(targetPostition);
            if (Mathf.Abs(cameraRotater.transform.rotation.eulerAngles.y - delta) > 355)
            {
                break;
            }
            yield return new WaitForSeconds(0.016f);
        }
    }
}
