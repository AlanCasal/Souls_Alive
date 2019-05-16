using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Manager : MonoBehaviour {
    public LevelManager lvlManager;

    void Start() {
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        lvlManager.lvlOnScene = 3;
        lvlManager.monstruosRestantes = 12;
    }
}