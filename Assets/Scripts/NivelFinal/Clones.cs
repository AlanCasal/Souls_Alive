using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clones : MonoBehaviour {
    public int hp;
    public Animator anim;
    public GameObject bossExplosion;
    public SpriteRenderer sprite;
    public LevelManager lvlManager;

    void Start() {        
        GetComponents();
        Instantiate(bossExplosion, transform.position, Quaternion.identity);
        if (transform.position.x > 0) sprite.flipX = true;
    }

    public GameObject PrefabRed;
    public GameObject PrefabGreen;
    Transform SpawnL;
    Transform SpawnR;
    public void GhostSpawn() {
        if (transform.position.x > 0) Instantiate(PrefabRed, SpawnL.position, Quaternion.identity);
        else Instantiate(PrefabGreen, SpawnR.position, Quaternion.identity);
    }

    public void ReceiveDamage(string typeOfDamage) { //child Body
        switch (typeOfDamage) {
            case "player":Hit1(); break;
            case "bala1": Hit1(); break;
            case "bala2": Hit2(); break;
        }
    }

    public GameObject Particles;
    void Hit1() {
        hp--;
        Instantiate(Particles, transform.position, Quaternion.identity);
        anim.Play("HitClon");
        if (hp <= 0) Die();
    }

    public GameObject Particles2;
    void Hit2() {
        Instantiate(Particles2, transform.position, Quaternion.identity);
        hp = hp - 3;
        anim.Play("HitClon");
        if (hp <= 0) Die();
    }

    public void Die() {
        Instantiate(bossExplosion, transform.position, Quaternion.identity);
        lvlManager.monstruosRestantes--;
        Destroy(gameObject);
    }

    void GetComponents() {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        SpawnL = transform.FindChild("SpawnL");
        SpawnR = transform.FindChild("SpawnR");
    }
}
