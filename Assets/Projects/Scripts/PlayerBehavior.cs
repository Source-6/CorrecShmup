using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionInfo))]
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction xAxis;
    private InputAction yAxis;
    [SerializeField] private float speed;
    [SerializeField] GameManager gameManager;
    private Camera cam;
    // private bool invincible;
    // private int life;


    public void Initialize(GameManager gameManager, Camera cam)
    {
        this.gameManager = gameManager;
        this.cam = cam;
        xAxis = actions.FindActionMap("Move").FindAction("xAxis");
        yAxis = actions.FindActionMap("Move").FindAction("yAxis");
        // invincible = false;
        // life = 3;
    }

    public void Process()
    {
        Moving();
        // if (life <= 0)
        // {
        //     life = 3;
        // }
    }

    void OnEnable()
    {
        actions.FindActionMap("Move").Enable();
    }

    void OnDisable()
    {
        actions.FindActionMap("Move").Disable();
    }

    void Moving()
    {
        float xMove = xAxis.ReadValue<float>();
        float yMove = yAxis.ReadValue<float>();
        Vector3 move = new Vector3(xMove, yMove, 0f);
        Vector3 movement = speed * Time.deltaTime * move;
        transform.Translate(movement);

        Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);


        screenPosition.x = Mathf.Clamp(screenPosition.x, 0f, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0f, Screen.height);

        transform.position = cam.ScreenToWorldPoint(screenPosition);
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (life > 0)
    //     {
    //         invincible = true;
    //         life -= 1;
    //         Debug.Log(life);

    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     invincible = false;
    // }


}
