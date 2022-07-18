using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;
    private ObstacleController obstacleController;
    private MeshRenderer meshRenderer;
    private Collider collision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }

    public void Shatter()
    {
        rb.isKinematic = false;
        collision.enabled = false;


        Vector3 forcePoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180f);

        rb.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * torque);
        rb.velocity = Vector3.down;
    }
}

