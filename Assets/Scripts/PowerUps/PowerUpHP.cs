using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHP : MonoBehaviour {
    public Player player;
    public LevelManager lvlManager;
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D info) {
        if (info.gameObject.layer == Layers.player && player.hp > 0) {
            lvlManager.powerUpsRemaining--;
            Destroy(gameObject);
        }
    }
}
