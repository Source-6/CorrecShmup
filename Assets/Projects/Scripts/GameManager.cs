using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private PlayerBehavior player;
    private List<EnemyBehavior> enemies = new();
    private float cooldown;
    private float chrono = 0f;
    private int playerLives;
    private LifeViewer lifeViewer;

    public void Initialize(Spawner spawner, float cooldown, PlayerBehavior player, int playerLives, LifeViewer lifeViewer)
    {
        this.spawner = spawner;
        this.cooldown = cooldown;
        this.player = player;
        this.playerLives = playerLives;
        this.lifeViewer = lifeViewer;
    }

    private void Update()
    {
        player.Process();
        chrono += Time.deltaTime;
        if (chrono >= cooldown)
        {
            chrono = 0f;
            EnemyBehavior enemy = spawner.Spawn();
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.Initialize(this);
            }
        }

        for (int index = 0; index < enemies.Count; index++)
        {
            enemies[index].Process();
        }
    }

    public void EnemyLeaveGame(EnemyBehavior enemy)
    {
        spawner.DeSpawn(enemy);
    }

    public void PlayerContact(GameObject other)
    {
        if (other.TryGetComponent(out EnemyBehavior enemy))
        {
            playerLives -= 1;
            if (playerLives >= 0) lifeViewer.UpdateImages(playerLives);
            EnemyLeaveGame(enemy);
        }
    }

}
