using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //*accessible everywhere
    // *- singleton
    //manage spawns
    //track/display points
    //track win/lose
    [SerializeField]
    private GameObject mushroomPrefab = null;
    [SerializeField]
    private GameObject bombPrefab = null;
    [SerializeField]
    private float spawnDelay = 2.0f;
    [SerializeField]
    private float spawnRate = 1.0f;
    [SerializeField]
    private RangeInt spawnAmount = new RangeInt(1, 3);
    [SerializeField]
    private float bombChance = 0.1f;
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;
    [SerializeField]
    private Vector3 spawnOffset = Vector3.zero;
    
    
    private float nextSpawnTime = 0f;
    
    private static GameManager instance = null;
    public static GameManager Instance {get{ return instance; }}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        nextSpawnTime = spawnDelay;
    }



    // Update is called once per frame
    void Update()
    {
        nextSpawnTime -= Time.deltaTime;
        if (nextSpawnTime <= 0.0f)
        {
            SpawnItems();
        }
    }
    
    void SpawnItems()
    {
        int numspawns = Random.Range(spawnAmount.start, spawnAmount.end + 1);
        for (int i = 0; i < numspawns; i++)
        {
            Vector3 spawnPos = spawnPosition;
            spawnPos.x += Random.Range(-spawnOffset.x, spawnOffset.x);
            spawnPos.y += Random.Range(-spawnOffset.y, spawnOffset.y);
            if (Random.Range(0.0f, 1.0f) < bombChance)
            {
                Instantiate(bombPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(mushroomPrefab, spawnPos, Quaternion.identity);
            }
        }
        nextSpawnTime = spawnDelay;
    }
}
