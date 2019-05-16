using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRedBrain : MonoBehaviour {
    void Start () {
        GetComponents();
        SetDirection();
    }
    
    void Update () {
        red.Move();
        red.Disabled_by_Player();
        Lvl_Final_Game_Over();
    }

    void SetDirection() {
        if (lvlManager.lvlOnScene == 4) red.movement.x = -1;
        else red.movement.x = 1;
    }

    public GhostRed red;
    public LevelManager lvlManager;
    void GetComponents() {
        red = GetComponent<GhostRed>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void Lvl_Final_Game_Over() {
        if (lvlManager.lvlOnScene == 4 && lvlManager.monstruosRestantes <= 0) red.Die();
    }
}
