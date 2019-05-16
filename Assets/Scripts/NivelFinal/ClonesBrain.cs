using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonesBrain : MonoBehaviour {
    public Clones clones;

	void Start () { clones = GetComponent<Clones>(); }
	
	void Update () { Actions(); }

    float spawnCD;
    float spawnDelay;
    public void Actions() {
        spawnCD += Time.deltaTime;
        spawnDelay += Time.deltaTime;
        clones.anim.SetFloat("Spawn_CD", spawnCD);

        if (spawnCD < 4 && spawnDelay > .6f) {
            clones.GhostSpawn();
            spawnDelay = 0;
        }
        else if (spawnCD > 10) spawnCD = 0;

        if (clones.hp <= 0) clones.Die();
    }
}
