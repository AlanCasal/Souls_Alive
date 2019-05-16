using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPurple : MonoBehaviour {

    public int hp;
    public float spd;

    void Start() {
        GetComponents();
        Instantiate(PrefabExplosion, transform.position, Quaternion.identity);
    }

    void Update() { Stomp(); }

    public Vector2 movement;
    public void Move() { rb.velocity = movement * spd; }

    public void ReceiveDamage(string typeOfDamage) { //child Body
        switch (typeOfDamage) {
            case "player":Hit1(); break;
            case "bala1": Hit1(); break;
            case "bala2": Hit2(); break;
        }
    }

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.waypoints) {
            transform.right = info.gameObject.transform.right;
            switch (info.gameObject.tag) {
                case "izq": movement = new Vector2(-1,0); break;
                case "der": movement = new Vector2(1, 0); break;
            }
        }
    }

    public Rigidbody2D rb;
    public Animator anim;
    public LevelManager lvlManager;
    AudioSource AudSrc;
    Transform left;
    Transform right;
    void GetComponents() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        AudSrc = GetComponent<AudioSource>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        left = transform.FindChild("Left");
        right = transform.FindChild("Right");
    }

    public GameObject Particles;
    void Hit1() {
        Instantiate(Particles, transform.position, Quaternion.identity);
        anim.Play("Disabled");
        hp--;
        if (hp <= 0) Die();
    }

    public GameObject Particles2;
    void Hit2() {
        Instantiate(Particles2, transform.position, Quaternion.identity);
        hp = hp - 3;
        if (hp <= 0) Die();
        else Disabled();
    }

    int disableTime;
    bool disabled;
    public void Disabled() {
        spd = 1;
        disabled = true;
        anim.SetBool("Disabled", disabled);
        anim.Play("Disabled");
        InvokeRepeating("DisableCheck", 1, 1);
        AudSrc.PlayOneShot(GameObject.Find("Audio").GetComponent<AudioManager>().GetSFX(12));
    }

    void DisableCheck() {
        disableTime++;
        if (disableTime == 2) {
            CancelInvoke();
            disableTime = 0;
            disabled = false;
            anim.SetBool("Disabled", disabled);
            spd = 9;
        }
    }

    public bool Stomped; //child Head
    void Stomp() { //check en Update
        if (Stomped) {
            hp--;
            Stomped = false;
        }
    }

    public GameObject PrefabExplosion;
    void Die() {
        Instantiate(PrefabExplosion, transform.position, Quaternion.identity);
        lvlManager.monstruosRestantes--;
        switch (lvlManager.lvlOnScene) {
            case 3: if (lvlManager.monstruosRestantes == -6) DropItems(); break;
        }
        Destroy(gameObject);
    }

    public GameObject dropHp;
    public GameObject dropSkill;
    void DropItems() {
        Instantiate(dropHp,    left.position,  Quaternion.identity);
        Instantiate(dropSkill, right.position, Quaternion.identity);
    }
}
