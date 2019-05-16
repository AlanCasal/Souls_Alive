using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Manager : MonoBehaviour {
    public LevelManager levelManager;

    void Start() {
        levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        levelManager.lvlOnScene = 2;
        levelManager.monstruosRestantes = 9;
    }
}