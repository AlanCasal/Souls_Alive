using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public int monstruosRestantes;

    void Start() { GetComponents(); }

    void Update() {
        Update_Player_Stats();
        ScenesCheck();
        Pausa();
        ActivateBoss();
        If_boss_dead();
    }

    public int powerUpsRemaining = 2;
    void ScenesCheck() {
        if (powerUpsRemaining <= 0) YouWin();
        else if (lvlOnScene == 4 && monstruosRestantes == 0) Creditos();

        else if (player.hp <= 0) YouLose();
    }

    void Pausa() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                pausePanel.SetActive(true);
                Time.timeScale = 0;

            }
            else if (Time.timeScale == 0) {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Resumir() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Reiniciar() {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    int delayEscena = 2;
    bool youwin;
    public void YouWin() {
        InvokeRepeating("Temporizar", 1, 1);
        if (delayEscena <= 0 && !youwin) {
            youWin.SetActive(true);
            audioManager.Snapshots("youwin");
            youwin = true;
        }
    }

    bool youlose;
    public void YouLose() {
        audioManager.playerHasDied = true;
        InvokeRepeating("Temporizar", 1, 1);
        if (delayEscena <= 0 && !youlose) {
            youLose.SetActive(true);
            audioManager.Snapshots("youlose");
            youlose = true;
        }
    }

    public void Creditos() {
        InvokeRepeating("Temporizar", 1, 1);
        if (delayEscena <= 0) SceneManager.LoadScene("Creditos");
    }

    bool skillLearned;
    public void NewSkill() {
        if (skillLearned) return;
        switch (lvlOnScene) {
            case 1: stompLearned.SetActive(true); break;
            case 2: atk2Learned.SetActive(true); break;
            case 3: ghostDisable.SetActive(true); break;            
        }
        audioManager.Snapshots("newskill");
        skillLearned = true;
    }

    public void NextLevel() {
        switch (lvlOnScene) {
            case 1: SceneManager.LoadScene("Nivel 2");     break;
            case 2: SceneManager.LoadScene("Nivel 3");     break;
            case 3: SceneManager.LoadScene("Nivel Final"); break;
        }
    }

    public void Temporizar() {
        delayEscena--;
        if (delayEscena == 0) CancelInvoke();
    }

    public int lvlOnScene;
    bool bossActivated;
    void ActivateBoss() {
        if (monstruosRestantes <= 0 && !bossActivated) {
            bossManager.ActivateBoss = true;
            audioManager.Snapshots("bossMusic");
            bossActivated = true;
        }
    }

    public bool bossIsDead;
    void If_boss_dead() {
        if (bossIsDead) {
            audioManager.Snapshots("bossdead");
            bossIsDead = false;
        }            
    }

    bool PlayerUpdated;
    void Update_Player_Stats() {
        if (!PlayerUpdated) {
            switch (lvlOnScene) {
                case 2:
                    player.hp = 2;
                    player.canStomp = true; break;

                case 3:
                    player.hp = 3;
                    player.canStomp = true;
                    player.Atk2Enabled = true; break;

                case 4:
                    player.hp = 4;
                    player.canStomp = true;
                    player.Atk2Enabled = true;
                    player.disableGhosts = true; break;
            }
            PlayerUpdated = true;
        }
    }

    public bool disableGhosts;    

    public GameObject pausePanel;
    public GameObject stompLearned;
    public GameObject youWin;
    public GameObject youLose;
    public GameObject atk2Learned;
    public GameObject ghostDisable;
    public AudioManager audioManager;
    public Player player;
    public BossManager bossManager;
    void GetComponents() {
        player = GameObject.Find("Player").GetComponent<Player>();
        bossManager = GetComponent<BossManager>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();

        pausePanel = GameObject.Find("Panel");
        pausePanel.SetActive(false);        
        youWin = GameObject.Find("You Win");
        youWin.SetActive(false);
        youLose = GameObject.Find("You Lose");
        youLose.SetActive(false);
        stompLearned = GameObject.Find("Stomp");
        stompLearned.SetActive(false);
        atk2Learned = GameObject.Find("Atk2");
        atk2Learned.SetActive(false);
        ghostDisable = GameObject.Find("GhostDisable");
        ghostDisable.SetActive(false);
    }
}