using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl1Manager : MonoBehaviour {
    public LevelManager levelManager;

    void Start() {
        levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        levelManager.lvlOnScene = 1;
        levelManager.monstruosRestantes = 5;
    }
}