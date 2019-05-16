using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSkill : MonoBehaviour {
    public Player player;
    public LevelManager lvlManager;
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D info) {
        if (info.gameObject.layer == Layers.player) {
            Skill();
            lvlManager.powerUpsRemaining--;
            Destroy(gameObject);
        }
    }

    void Skill() {
        switch (lvlManager.lvlOnScene) {
            case 1: player.canStomp = true; break;
            case 2: player.Atk2Enabled = true; break;
            case 3: break;
        }
    }
}
