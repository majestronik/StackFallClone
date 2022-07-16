using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 cameraPos;
    private Transform player, win;

    private float cameraOffset = 4f;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
        }

        if (transform.position.y > player.position.y - 3 && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y + 3, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, transform.position.z);
        }
    }
}
