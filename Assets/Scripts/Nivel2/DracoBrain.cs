using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DracoBrain : MonoBehaviour {
    public Draco draco;
    void Start() { draco = GetComponent<Draco>(); }

    void Update() { Actions(); }

    bool moving;
    public void Actions() {
        if (draco.alive && !moving) {
            draco.CanMove = true;
            draco.m_collider.enabled = true;
            moving = true;
        }
        draco.Move();
    }
}
