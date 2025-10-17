using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    public void Initialize(Vector3 position, Vector3 direction, float speed, GameManager gameManager)
    {
        this.speed = speed;
        this.direction = direction;
        transform.position = position;
        // this.gameManager = gameManager;
    }

    public void Process()
    {
        Vector3 movement = speed * Time.deltaTime * direction;
        transform.Translate(movement, Space.World);
        transform.up = direction;
    }

}
