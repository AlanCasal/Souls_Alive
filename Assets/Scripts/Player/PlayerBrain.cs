using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour {
    public Player player;
    void Start() { player = GetComponent<Player>(); }

    void Update() {
        CheckKeys();
        HPhearts();
    }

    float Atk1Delay;
    float Atk2Delay;
    float disable_Ghosts_Delay;
    public void CheckKeys() {
        if (player.CanMove && Time.timeScale == 1) { //no está en pausa
            Move();
            Jump();
            Atk1();            
            if (player.Atk2Enabled) Atk2(); //nivel 3+
            if (player.disableGhosts) DisableGhosts(); //nivel 4+
        }
    }

    void HPhearts() {
        switch (player.hp) {
            case 4:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true); break;

            case 3:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false); break;

            case 2:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false); break;

            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false); break;
        }
    }

    void Move() {
        Vector2 MovHorizontal = Vector2.zero;
        if (Input.GetAxis("Horizontal") != 0) {
            MovHorizontal = Vector2.right * Input.GetAxis("Horizontal");
            player.Move(MovHorizontal);
        }
    }

    void Jump() { if (Input.GetKeyDown(KeyCode.UpArrow)) player.Jump(); }

    void Atk1() {
        Atk1Delay += Time.deltaTime; //Atk común
        if (Input.GetKeyDown(KeyCode.Q) && Atk1Delay > .1f) {
            player.Attack1();
            Atk1Delay = 0;
        }
    }

    void Atk2() {
        if (player.Atk2Enabled) { //Bala especial, nivel 3+
            Atk2Delay += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.W) && Atk2Delay > 3) {
                player.Attack2();
                Atk2Delay = 0;
            }
        }
    }

    void DisableGhosts() {
        disable_Ghosts_Delay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && disable_Ghosts_Delay > 10) {
            player.DisableGhosts();
            disable_Ghosts_Delay = 0;
        }
    }
}