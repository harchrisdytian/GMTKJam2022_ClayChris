using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;

    void SpawnEnemy()
    {
       var pos = transform.GetChild(Random.Range(0, transform.childCount)).localPosition;
        
       var spawn = Instantiate(enemy, pos, Quaternion.identity);
    }
    private void Start()
    {
        SpawnEnemy();
    }
}
