using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoint;
    public Transform[] portalSpawnPoints;
    public Transform[] poisonSpawnPoints;
    public GameObject portal;
    public GameObject poison;

    private int rand;
    private int randPosition;
    private int portalRandPosition;
    private int poisonRandPosition;

    public float startTimeBtwSpawns;
    private float timeBtwSpawns;
    private float portalTimeBtwSpawns;
    private float poisonTimeBtwSpawns;

    public int minSec, maxSec;
    private int minSecPortal, maxSecPortal, portalTimes = 0, poisonTimes = 0;
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        portalTimeBtwSpawns = Random.Range(20,30);
        poisonTimeBtwSpawns = Random.Range(20, 30);

        minSec = 6;
        maxSec = 9;

        minSecPortal = 35;
        maxSecPortal = 90;
    }

    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            rand = Random.Range(0, enemies.Length);
            randPosition = Random.Range(0, spawnPoint.Length);
            Instantiate(enemies[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
            timeBtwSpawns = Random.Range(minSec,maxSec);
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

        if (portalTimes <= 4)
        {
            if (portalTimeBtwSpawns <= 0)
            {
                portalTimes++;
                portalRandPosition = Random.Range(0, portalSpawnPoints.Length);
                Instantiate(portal, portalSpawnPoints[portalRandPosition].transform.position, Quaternion.identity);
                portalTimeBtwSpawns = Random.Range(minSecPortal, maxSecPortal);
            }
            else
            {
                portalTimeBtwSpawns -= Time.deltaTime;
            }
        }

        if(poisonTimes <= 6)
        {
            if (poisonTimeBtwSpawns <= 0)
            {
                poisonTimes++;
                poisonRandPosition = Random.Range(0, poisonSpawnPoints.Length);
                Instantiate(poison, poisonSpawnPoints[poisonRandPosition].transform.position, Quaternion.identity);
                poisonTimeBtwSpawns = Random.Range(minSecPortal - 3, maxSecPortal - 3);
            }
            else
            {
                poisonTimeBtwSpawns -= Time.deltaTime;
            }
        }
    }
} 
