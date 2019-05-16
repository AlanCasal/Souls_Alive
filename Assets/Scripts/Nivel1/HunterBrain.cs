using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterBrain : MonoBehaviour {
    public Hunter hunter;
	void Start () {
        hunter = GetComponent<Hunter>();
	}
    
	void Update () {
        Actions();
    }

    public Vector2 move;
    float ShootDelay;
    public void Actions() {
        if (hunter.CanMove) {
            hunter.Move(move);

            ShootDelay += Time.deltaTime;
            if (ShootDelay > 1.5) {
                hunter.Shoot();
                ShootDelay = 0;
            }
        }
    }
}
