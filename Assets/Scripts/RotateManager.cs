using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public float speed = 100f;

    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime));
    }
}
