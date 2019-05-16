using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GastlyBrain : MonoBehaviour {
    public Gastly gastly;

    void Start () {
        gastly = GetComponent<Gastly>();        
    }

    public Vector2 move;
    void Update () {        
        gastly.Move(move);
	}
}
