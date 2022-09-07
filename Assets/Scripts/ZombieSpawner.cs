using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zombie;
    float randX;
    Vector2 whereToSpawn;
    public float spawnRate;
    float nextSpawn = 0.0f;
    public float minX, maxX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn){
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(minX, maxX);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(zombie, whereToSpawn, Quaternion.identity);
        }
    }
}
