using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour {    
    void Start() { GetComponents(); }

    void Update() { Actions(); }

    float spawnCD;
    float spawnDelay;
    public void Actions() {
        spawnCD += Time.deltaTime;
        spawnDelay += Time.deltaTime;
        boss.anim.SetFloat("Spawn_CD", spawnCD);

        SpawnGhosts();
    }

    void SpawnGhosts() {
        if (spawnCD < 4 && spawnDelay > .8f) {
            boss.SpawnGhosts();
            spawnDelay = 0;
        }
        else if (spawnCD > 10) spawnCD = 0;
    }

    public Boss boss;
    public LevelManager lvlManager;
    void GetComponents() {
        boss = GetComponent<Boss>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }
}
