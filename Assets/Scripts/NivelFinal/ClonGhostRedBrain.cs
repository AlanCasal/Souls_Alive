using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonGhostRedBrain : MonoBehaviour {
    public ClonGhostRed red;
    public Vector2 move;
    public LevelManager lvlManager;

    void Start() {
        red = GetComponent<ClonGhostRed>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void Update() {
        move = red.movement;
        red.Move(move);
        red.Disabled_by_Player();
        Lvl_Final_Game_Over();
    }

    void Lvl_Final_Game_Over() {
        if (lvlManager.lvlOnScene == 4 && lvlManager.monstruosRestantes <= 0) red.Die();
    }
}
