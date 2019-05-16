using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int hp;

    void Start() {
        GetComponents();
        LeftArm = transform.FindChild("LeftArm"); 
        RightArm = transform.FindChild("RightArm"); //son para los fantasmas spawneen en sus manos
    }

    public GameObject PrefabRed;
    public GameObject PrefabGreen;
    Transform LeftArm;
    Transform RightArm;    
    public void SpawnGhosts() {
        Instantiate(  PrefabRed,  LeftArm.position, Quaternion.identity);
        Instantiate(PrefabGreen, RightArm.position, Quaternion.identity);
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
        anim.Play("Hit");
        if (hp <= 0) EffectsOnDeath();
    }

    public GameObject Particles2;
    void Hit2() {
        Instantiate(Particles2, transform.position, Quaternion.identity);
        hp = hp - 3;
        anim.Play("Hit");
        if (hp <= 0) EffectsOnDeath();
    }

    public Animator anim;
    public LevelManager lvlManager;
    void GetComponents() {
        anim = GetComponent<Animator>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    public GameObject death;
    public GameObject PrefabClone;
    public GameObject PowerUp;
    void EffectsOnDeath(){
        if (hp <= 0) {
            GameObject powerUp = Instantiate(PowerUp);
            powerUp.transform.position = transform.position;

            Instantiate(death, transform.position, Quaternion.identity);
            //Instantiate(PowerUp, transform.position, Quaternion.identity);

            Instantiate(PrefabClone, transform.position = new Vector3(8, -3.75f, 0), Quaternion.identity);
            Instantiate(PrefabClone, transform.position = new Vector3(-7, .5f, 0), Quaternion.identity);

            lvlManager.monstruosRestantes--;
            Destroy(gameObject);
        }
    }
}
