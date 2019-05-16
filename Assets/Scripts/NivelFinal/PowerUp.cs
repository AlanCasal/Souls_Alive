using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    public Animator anim;

    void Start () { anim = GetComponent<Animator>(); }

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.player && info.gameObject.tag == "Untagged") anim.SetBool("PickUp", true);
    }
}
