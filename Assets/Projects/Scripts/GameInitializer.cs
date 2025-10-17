using System;
using UnityEngine.UI;
using UnityEngine;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Vector3 camPosition;
    [SerializeField] Quaternion camRotation;

    [Space]
    [Header("Spawner")]
    [SerializeField] Spawner spawner;
    [SerializeField] float forwardSpawn = 20f;
    [SerializeField] EnemyBehavior enemyPrefab;
    [SerializeField] int batchNumber;
    [SerializeField] float cooldown;

    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Space]
    [Header("Player")]
    [SerializeField] PlayerBehavior player;
    [SerializeField] int playerLives = 3;

    [Space]
    [Header("UI")]
    [SerializeField] LifeViewer lifeCanva;
    [SerializeField] Image lifeImage;
    [SerializeField] Vector2 firtImagePos;
    [SerializeField] Vector2 imageOffset;



    void Start()
    {
        CreateObjects();
        InitializeObjects();
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        spawner = Instantiate(spawner);
        player = Instantiate(player);
        gameManager = Instantiate(gameManager);
        lifeCanva = Instantiate(lifeCanva);
    }

    private void InitializeObjects()
    {
        cameraManager.Initialize(camPosition, camRotation);
        (Vector3 min, Vector3 max) = cameraManager.GetRightBorderPoints(forwardSpawn);
        spawner.Initialize(enemyPrefab, min, max, batchNumber);
        player.Initialize(gameManager, cameraManager.Cam);
        player.gameObject.SetActive(true);
        
        player.GetComponent<PlayerCollisionInfo>().Initialize(gameManager);

        gameManager.Initialize(spawner, cooldown, player, playerLives, lifeCanva);
        lifeCanva.Initialise(lifeImage, playerLives, firtImagePos, imageOffset);

    }

}
