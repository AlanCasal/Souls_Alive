using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Gastly : MonoBehaviour {
    public int hp;
    public float spd;
    public Rigidbody2D rb;
    public LevelManager lvlManager;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
    }

    void Update() { Stomp(); }

    public bool Stomped; //lo activa child head
    void Stomp() {
        if (Stomped) Hit();
        Stomped = false;
    }

    public void Move(Vector2 movement) { rb.velocity = movement * -spd; }

    public void OnTriggerEnter2D(Collider2D info) {
        switch (info.gameObject.layer) {
            case Layers.waypoints:
                transform.right = info.gameObject.transform.right;
                spd = spd * -1; break;

            case Layers.player:
                if (info.gameObject.tag == "bala1") Destroy(info.gameObject);
                Hit(); break;
        }
    }

    public GameObject explosion;
    public GameObject particles;    
    void Hit() {
        hp--;
        Instantiate(particles, transform.position, Quaternion.identity);
        Instantiate(explosion, transform.position, Quaternion.identity);
        lvlManager.monstruosRestantes--;
        Destroy(gameObject);
    }
}
