using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPurpleBrain : MonoBehaviour
{
    void Start() { GetComponents(); }

    void Update() { purple.Move(); }

    public GhostPurple purple;
    void GetComponents() { purple = GetComponent<GhostPurple>(); }
}
