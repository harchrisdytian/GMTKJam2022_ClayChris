using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] Enemy;
    public GameObject Player, playerBody;
    [Header("Spawns")]
    public Transform[] spawns;
    public float maxRate,minRate,enemySpawnRate;

    public Button PlayButton;

    private int playerSpawnNumber, enemySpawnNumber;
    private bool gameOver;

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
            Enemy[enemySpawnNumber].SetActive(true);
            playerSpawnNumber = -1;
            StartCoroutine(GameLoop());
        }
        else
        {
            SpawnEnemy();
        }
    }

    IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(5);

        while (!gameOver)
        {
            enemySpawnNumber = Random.Range(0, spawns.Length);
            Enemy[enemySpawnNumber].SetActive(true);
            enemySpawnRate = Mathf.Clamp(enemySpawnRate,minRate, maxRate);
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
}
