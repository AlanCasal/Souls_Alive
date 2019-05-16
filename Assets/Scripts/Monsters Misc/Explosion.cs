using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float Duracion;

    void Start() {
        Destroy(gameObject, Duracion);
    }
}
