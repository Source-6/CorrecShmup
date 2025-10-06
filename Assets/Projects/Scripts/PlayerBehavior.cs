using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction xAxis;
    private InputAction yAxis;
    [SerializeField] private float speed;
    [SerializeField] GameManager gameManager;
    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        xAxis = actions.FindActionMap("Move").FindAction("xAxis");
        yAxis = actions.FindActionMap("Move").FindAction("yAxis");
    }

    public void Process()
    {
        Moving();
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
        transform.position += speed * Time.deltaTime * move;
    }

}
