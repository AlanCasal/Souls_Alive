using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Hunter : MonoBehaviour {
    public int hp;
    public float spd;
    public bool CanMove = true;

    void Start () {
        GetComponents();
        Instantiate(Particles, transform.position, Quaternion.identity);
    }

    void Update() {
        DisableCheck();
        Death();
        Stomp();
    }

    public void Move(Vector2 movement) {
        if (CanMove) {
            anim.Play("HunterMove");
            rb.velocity = movement * spd;
        }
    }

    public void ReceiveDamage(string typeOfDamage) { //child Body
        switch (typeOfDamage) {
            case "player":Hit1(); break;
            case "bala1": Hit1(); break;
            case "bala2": Hit2(); break;
        }
    }

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.waypoints && info.gameObject.tag == "hunter") {
            transform.right = info.gameObject.transform.right;
            spd = spd * -1;
        }
    }

    public GameObject prefabBullet;
    public void Shoot() {
        anim.Play("HunterShoot");
        InstantiateBullet();
    }
    
    public GameObject explosion;
    public GameObject Particles;
    void Hit1() {
        Instantiate(Particles, transform.position, Quaternion.identity);
        hp--;
        Disabled();
    }

    void Hit2() {
        Instantiate(Particles, transform.position, Quaternion.identity);
        hp = hp - 3;
        Disabled();
    }

    void Death() {
        if (hp <= 0) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            lvlManager.monstruosRestantes--;

            switch (lvlManager.lvlOnScene) {
                case 1: DropItems(); lvlManager.bossIsDead = true; break;
                case 3: if (lvlManager.monstruosRestantes == -6) DropItems(); break;
            }
            Destroy(gameObject);
        }
    }

    float disableTime;
    void Disabled() {
        CanMove = false;
        rb.velocity = Vector2.zero;
        disableTime = 0;
    }

    void DisableCheck() {
        disableTime += Time.deltaTime;
        if (disableTime > .1) CanMove = true;
    }

    public bool Stomped;
    void Stomp() {
        if (Stomped) hp--;
        Stomped = false;
    }

    void InstantiateBullet() {
        GameObject lightBall = Instantiate(prefabBullet, transform.position, Quaternion.identity);
        Bullet disparar = lightBall.GetComponent<Bullet>();
        if (rb.velocity.x < 0) disparar.direccion = -1;
        lightBall.transform.position += transform.right * 1;
    }

    public GameObject dropHp;
    public GameObject dropSkill;
    Transform left;
    Transform right;
    void DropItems() {
        Instantiate(dropHp, left.position, Quaternion.identity);
        Instantiate(dropSkill, right.position, Quaternion.identity);
    }

    public Rigidbody2D rb;
    public Animator anim;
    public LevelManager lvlManager;

    void GetComponents() {
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left = transform.FindChild("Left");
        right = transform.FindChild("Right");
    }
}