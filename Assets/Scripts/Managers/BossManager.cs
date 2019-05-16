using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {
    public GameObject hunter;
    public GameObject purple;

    public LevelManager lvlManager;
    void Start() { lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>(); }
        
    void Update() {
        PrepareBoss();
        GenerateBoss();
    }

    bool BossActivated; 
    public bool ActivateBoss; //lo activa el manager de gral levels
    public void GenerateBoss() {
        if (ActivateBoss & !BossActivated) {
            switch (lvlManager.lvlOnScene) {
                case 1: BossLvl1(); break;
                case 2: BossLvl2(); break;
                case 3: BossLvl3(); break;
            }
            BossActivated = true; //lo activa esta clase para que triggeree una sola vez
        }
    }

    void BossLvl1() { Instantiate(hunter, transform.position = new Vector2(7.8f, 1.8f), Quaternion.identity); }
    void BossLvl2() { dracoLvl2.alive = true; }
    void BossLvl3() {
        Instantiate(hunter, transform.position = new Vector2(-7.15f, .55f), Quaternion.identity);
        Instantiate(hunter, transform.position = new Vector2(-7.15f, -3.73f), Quaternion.identity);
        Instantiate(purple, transform.position = new Vector2(-12.93f, 2.9f), Quaternion.identity);
        Instantiate(purple, transform.position = new Vector2(-12.93f, -1.57f), Quaternion.identity);
    }

    public Draco dracoLvl2;
    bool BossPrepared;
    void PrepareBoss() {
        if (!BossPrepared)
            if (lvlManager.lvlOnScene == 2) dracoLvl2 = GameObject.Find("Draco").GetComponent<Draco>();
        
        BossPrepared = true;
    }
}
