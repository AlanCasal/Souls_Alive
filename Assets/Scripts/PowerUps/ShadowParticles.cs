using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowParticles : MonoBehaviour {
    public Player player;
    ParticleSystem particle;

    void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        particle = GetComponent<ParticleSystem>();
    }
	
	void Update () {
        transform.position = player.transform.position;
        if (!particle.IsAlive()) Destroy(gameObject);
	}
}