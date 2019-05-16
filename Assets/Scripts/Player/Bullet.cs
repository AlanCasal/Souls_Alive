using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float spd;
    public int direccion = 1;
    public Rigidbody2D rb;
    Vector2 movement;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        movement.x = 1;
        Destroy(gameObject, 0.5f);
    }

    void Update () { rb.velocity = movement * spd * direccion; }
}
