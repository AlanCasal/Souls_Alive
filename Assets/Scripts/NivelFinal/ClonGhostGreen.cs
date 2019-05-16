using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonGhostGreen : MonoBehaviour {
    public int hp;
    public float spd;
    public GameObject PrefabExplosion;

    void Start() {
        GetComponents();
        movement.x = 1;
        Instantiate(PrefabExplosion, transform.position, Quaternion.identity);
    }

    public void Move(Vector2 movement) { rb.velocity = movement * spd; }

    public void ReceiveDamage(string typeOfDamage) { //child Body
        switch (typeOfDamage) {
            case "player":Hit1(); break;
            case "bala1": Hit1(); break;
            case "bala2": Hit2(); break;
        }
    }

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.waypointsG) {
            transform.right = info.gameObject.transform.right;
            switch (info.gameObject.tag) {
                case "subir": movement = new Vector2(0, 1); break;
                case "bajar": movement = new Vector2(0,-1); break;
                case "izq":   movement = new Vector2(-1,0); break;
                case "der":   movement = new Vector2(1, 0); break;
            }
        }
    }

    public GameObject Particles;
    void Hit1() {
        hp--;
        Instantiate(Particles, transform.position, Quaternion.identity);
        anim.Play("Hit");
        if (hp <= 0) Die();
    }

    public GameObject Particles2;
    void Hit2() {
        Instantiate(Particles2, transform.position, Quaternion.identity);
        hp = hp - 3;
        anim.Play("Hit");
        if (hp <= 0) Die();
    }

    public Rigidbody2D rb;
    public Vector2 movement;
    public Animator anim;
    public LevelManager lvlManager;
    void GetComponents() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    public void Die() {
        Instantiate(PrefabExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    bool disabled;
    public void Disabled_by_Player() {
        if (lvlManager.disableGhosts) {
            spd = 1;
            hp = 1;
            disabled = true;
            anim.SetBool("Disabled", disabled);
            anim.Play("Disabled");
            InvokeRepeating("DisableTimer", 1, 1);
        }
    }

    int disableTime;
    void DisableTimer() {
        disableTime++;
        if (disableTime >= 5) {
            CancelInvoke();
            disableTime = 0;
            disabled = false;
            anim.SetBool("Disabled", disabled);
            spd = 3;
            hp = 3;
            lvlManager.disableGhosts = false;
        }
    }
}
