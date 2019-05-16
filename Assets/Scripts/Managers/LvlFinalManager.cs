using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlFinalManager : MonoBehaviour {
    public LevelManager lvlManager;

    void Start() {
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        lvlManager.lvlOnScene = 4;
        lvlManager.monstruosRestantes = 3;
    }
}
