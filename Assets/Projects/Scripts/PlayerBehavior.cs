using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionInfo))]
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction xAxis;
    private InputAction yAxis;
    private InputAction shoot;
    private InputAction targetAction;
    [SerializeField] private float speed;
    [SerializeField] GameManager gameManager;
    private Camera cam;
    private BulletBehavior bullet;


    public void Initialize(GameManager gameManager, Camera cam, BulletBehavior bullet)
    {
        this.gameManager = gameManager;
        this.cam = cam;
        xAxis = actions.FindActionMap("Move").FindAction("xAxis");
        yAxis = actions.FindActionMap("Move").FindAction("yAxis");
        shoot = actions.FindActionMap("Move").FindAction("Shoot");
        targetAction = actions.FindActionMap("Move").FindAction("Target");
        this.bullet = bullet;

    }

    public void Process()
    {
        Moving();

    }

    void OnEnable()
    {
        actions.FindActionMap("Move").Enable();
        actions.FindActionMap("Move").FindAction("Shoot").performed += Shoot;
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

    private void Shoot(InputAction.CallbackContext callbackContext)
    {
        Vector3 targetPosition = targetAction.ReadValue<Vector2>();
        targetPosition.z = transform.position.z - cam.transform.position.z;

        targetPosition = cam.ScreenToWorldPoint(targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;
        BulletBehavior newbullet = Instantiate(bullet);
        newbullet.Initialize(transform.position, direction, 5f);
        gameManager.AddBullet(newbullet);
    }



}
