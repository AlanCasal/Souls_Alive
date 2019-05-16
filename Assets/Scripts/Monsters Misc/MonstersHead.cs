using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersHead : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.player && info.gameObject.tag == "Untagged") Stomped();
    }
    
    void Stomped() {
        switch (transform.parent.tag) {
            case "gastly":      transform.parent.GetComponent<Gastly>().Stomped = true; break;
            case "hunter":      transform.parent.GetComponent<Hunter>().Stomped = true; break;
            case "draco":        transform.parent.GetComponent<Draco>().Stomped = true; break;
            case "purple": transform.parent.GetComponent<GhostPurple>().Stomped = true; break;
        }
    }
}
