using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] skillPickups;
    public float spawnRadius = 5f;
    private float nextSpawnTime;

    void Start() {
        ScheduleNextSpawn();
    }

    void Update() {
        if (Time.time >= nextSpawnTime) {
            SpawnSkillObject();
            ScheduleNextSpawn();
        }
    }

    void ScheduleNextSpawn() {
        nextSpawnTime = Time.time + Random.Range(5f, 6f);
    }

    void SpawnSkillObject()
    {
        Transform player = FindObjectOfType<Playercontroller>().transform;
        float offsetX = Random.Range(3f, 6f); //tentukan range
        if (Random.value < 0.5f) offsetX *= -1; //random kiri/kanan
        Vector2 spawnPos = new Vector2(player.position.x + offsetX, player.position.y);//hanya x yang diambil
        Instantiate(skillPickups[Random.Range(0, skillPickups.Length)], spawnPos, Quaternion.identity);
    }
}

