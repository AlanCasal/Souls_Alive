using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour {
    public float Duracion;

    void Start() {
        Destroy(gameObject, Duracion);
    }
}
