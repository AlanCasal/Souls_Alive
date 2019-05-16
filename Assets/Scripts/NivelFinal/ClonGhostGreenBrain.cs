using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonGhostGreenBrain : MonoBehaviour {
    public ClonGhostGreen green;
    public Vector2 move;
    public LevelManager lvlManager;

    void Start() {
        green = GetComponent<ClonGhostGreen>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void Update() {
        move = green.movement;
        green.Move(move);
        green.Disabled_by_Player();
        Lvl_Final_Game_Over();
    }

    void Lvl_Final_Game_Over() {
        if (lvlManager.lvlOnScene == 4 && lvlManager.monstruosRestantes <= 0) green.Die();
    }
}
