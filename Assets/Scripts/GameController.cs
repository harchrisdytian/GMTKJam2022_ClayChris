using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Enemy;
    public GameObject Player, playerBody;
    [Header("Spawns")]
    public Transform[] spawns;

    public Button PlayButton;

    private int playerSpawnNumber, enemySpawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        int randomSpawnIndex = Random.Range(0, spawns.Length);
        Instantiate(Player, spawns[randomSpawnIndex].position, Quaternion.identity);
        SpawnPlayer();
        SpawnEnemy();

    }

    public void SpawnPlayer()
    {
        playerSpawnNumber = Random.Range(0, spawns.Length);
        Player.transform.position = spawns[playerSpawnNumber].position;
        playerBody.SetActive(true);
    }

    public void SpawnEnemy()
    {
        enemySpawnNumber = Random.Range(0, spawns.Length);
        if(enemySpawnNumber != playerSpawnNumber)
        {
            Instantiate(Enemy, spawns[enemySpawnNumber].position, Quaternion.identity);
            playerSpawnNumber = -1;
        }
        else
        {
            SpawnEnemy();
        }
    }
}
