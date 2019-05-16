using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGreenbrain : MonoBehaviour {
    void Start() { GetComponents(); }

    void Update() {
        SetDirection();
        green.Move();
        green.Disabled_by_Player();
        Lvl_Final_Game_Over();
    }

    bool DirectionSet;
    void SetDirection() {
        if (!DirectionSet) {
            if (lvlManager.lvlOnScene == 3) green.movement.x = -1;
            else green.movement.x = 1;
            DirectionSet = true;
        }
    }

    public GhostGreen green;
    public LevelManager lvlManager;
    void GetComponents() {
        green = GetComponent<GhostGreen>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void Lvl_Final_Game_Over() {
        if (lvlManager.lvlOnScene == 4 && lvlManager.monstruosRestantes <= 0) green.Die();
    }
}
