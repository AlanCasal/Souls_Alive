using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour {    
    public float spd;
    public int hp;

    void Start () {
        GetComponents();
        anim.SetInteger("HP", hp);

        CanMove = true;
    }

    void Update() {
        PowerUpCheck();
        DisableCheck();
    }

    public void OnCollisionEnter2D(Collision2D col) {
        switch (col.gameObject.tag) {
            case "pisos": NotJumping(); break;
            case "newSkill": LearnSkill(); break; //la nueva skill en cada lvl
            case "hpBoost": HPboost(); break; //boost de hp en cada lvl
            case "precipicio": Die(); break;
        }
    }

    public void OnTriggerEnter2D(Collider2D info) {
        switch (info.gameObject.layer) {
            case Layers.monsters: Hit();          break;
            case Layers.PowerUp:  PowerUpFinal(); break;
        }        
        if (info.gameObject.tag == "head") Stomp();        
    }

    public SpriteRenderer sprite;
    public bool CanMove;
    public void Move(Vector2 horizontal) {
        Vector2 movement;
        movement.x = horizontal.x * spd;
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        anim.SetFloat("RunSpd", Mathf.Abs(horizontal.x));
        anim.SetFloat("JumpSpd", rb.velocity.y);

        if (horizontal.x != 0) {
            if (!AudSrc.isPlaying && !isJumping) AudSrc.PlayOneShot(audioManager.GetSFX(10));
            if (horizontal.x < 0) sprite.flipX = true;
            else if (horizontal.x > 0) sprite.flipX = false;
        }
    }

    bool isJumping;
    public Vector2 jumpStr;
    public void Jump() {
        if (!isJumping) {
            rb.velocity = jumpStr;
            isJumping = true;
            anim.SetBool("OnAir", true);

            AirAnimation();

            AudSrc.PlayOneShot(audioManager.GetSFX(2));
        }
    }

    void NotJumping() {
        isJumping = false;
        anim.SetBool("OnAir", isJumping);
        GroundAnimation();
    }

    public GameObject prefabBullet;
    public void Attack1() {
        AtkAnimation();
        InstantiateBullet1();
    }

    public GameObject Particles;
    void Hit() {
        if (!powerUpActive) {
            Disabled();
            Instantiate(Particles, transform.position, Quaternion.identity);
            hp--;
            anim.SetInteger("HP", hp);
            HitAnimation();
            if (hp > 0) AudSrc.PlayOneShot(audioManager.GetSFX(3));
        }
        if (hp <= 0) Die();
    }

    void Die() {
        CanMove = false;
        hp = 0; //la pongo en 0 porque el precipicio no le saca vida pero igual lo mata
        anim.Play("Death");
    }
    public bool canStomp;
    public bool Atk2Enabled;
    void LearnSkill() {
        AudSrc.PlayOneShot(audioManager.GetSFX(5));
        switch (lvlManager.lvlOnScene) {
            case 1:
                canStomp = true;
                anim.SetBool("StompLearned", canStomp); 
                anim.Play("NewSkill"); break;
            case 2:
                Atk2Enabled = true;
                anim.SetBool("Atk2Learned", Atk2Enabled);
                anim.Play("NewSkill"); break;
            case 3:
                disableGhosts = true;
                anim.SetBool("DisableGhosts", disableGhosts); 
                anim.Play("NewSkillFinal"); break;
        }
    }

    void HPboost() {
        anim.Play("hpBoost");
        AudSrc.PlayOneShot(audioManager.GetSFX(4));
        switch (lvlManager.lvlOnScene) {
            case 1: hp = 2; break;
            case 2: hp = 3; break;
            case 3: hp = 4; break;
        }
    }

    public void Stomp() {
        AudSrc.PlayOneShot(GameObject.Find("Audio").GetComponent<AudioManager>().GetSFX(6));
        rb.velocity = jumpStr;
        isJumping = true;
        anim.SetBool("OnAir", isJumping);
    } //nivel 2+

    public GameObject prefabBullet2;
    public void Attack2() {
        AtkAnimation();
        InstantiateBullet2();
    } //nivel 3+

    void Disabled() {
        CanMove = false;
        canStomp = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        disableTime = 0;
    }

    float disableTime;
    void DisableCheck() {
        disableTime += Time.deltaTime;

        if (disableTime > .5) {
            rb.gravityScale = 4;
            CanMove = true;
            if (lvlManager.lvlOnScene > 1) canStomp = true;
        }
    }

    public GameObject particles2;
    public bool disableGhosts;
    public void DisableGhosts() {
        AudSrc.PlayOneShot(audioManager.GetSFX(14));
        anim.Play("NewSkillFinal");
        Instantiate(particles2, transform.position, Quaternion.identity);
        lvlManager.disableGhosts = true;
    }

    bool powerUpActive;
    public GameObject ShadowParticles;
    void PowerUpFinal() {        
        AudSrc.PlayOneShot(audioManager.GetSFX(13));
        powerUpActive = true;
        anim.SetBool("PowerUp", true);
        InvokeRepeating("PowerUpTimer", 1, 1);
        Instantiate(ShadowParticles, transform.position, Quaternion.identity);
    }

    float powerUpTime;
    void PowerUpTimer() { powerUpTime++; }

    void PowerUpCheck() {
        if (powerUpTime >= 15) {
            CancelInvoke();
            anim.SetBool("PowerUp", false);
            powerUpActive = false;
        }
    }

    public Rigidbody2D rb;
    public Animator anim;
    public LevelManager lvlManager;
    AudioSource AudSrc;
    public AudioManager audioManager;
    void GetComponents() {
        rb = GetComponent<Rigidbody2D>();
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        anim = GetComponent<Animator>();
        AudSrc = GetComponent<AudioSource>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }

    void AtkAnimation() {
        if (isJumping) {
            switch (lvlManager.lvlOnScene) {
                case 1: anim.Play("AtkAir"); break;
                case 2: anim.Play("AtkAirStomp"); break;
                case 3: anim.Play("AtkAir2"); break;
                case 4:
                    if (powerUpActive) anim.Play("AtkAirPU");
                    else anim.Play("AtkAir2"); break;
            }
        }

        else {
            switch (lvlManager.lvlOnScene) {
                case 1: anim.Play("AtkGround"); break;
                case 2: anim.Play("AtkGroundStomp"); break;
                case 3: anim.Play("AtkGround2"); break;
                case 4:
                    if (powerUpActive) anim.Play("AtkGroundPU");
                    else anim.Play("AtkGround2"); break;
            }
        }
    }

    void AirAnimation() {
        switch (lvlManager.lvlOnScene) {
            case 1: anim.Play("Air"); break;
            case 2: anim.Play("AirStomp"); break;
            case 3: anim.Play("Air2"); break;
            case 4:
                if (powerUpActive) anim.Play("AirPU");
                else anim.Play("Air2"); break;
        }
    }

    void HitAnimation() {
        switch (lvlManager.lvlOnScene) {
            case 1: anim.Play("Hit"); break;
            case 2: anim.Play("HitStomp"); break;
            case 3: anim.Play("Hit2"); break;
            case 4: anim.Play("Hit2"); break;
        }
    }

    void GroundAnimation() {
        switch (lvlManager.lvlOnScene) {
            case 1: anim.Play("Ground"); break;
            case 2: anim.Play("GroundStomp"); break;
            case 3: anim.Play("Ground2"); break;
            case 4:
                if (powerUpActive) anim.Play("GroundPU");
                else anim.Play("Ground2"); break;
        }
    }

    void InstantiateBullet1() {
        GameObject lightBall = Instantiate(prefabBullet, transform.position, Quaternion.identity);
        lightBall.transform.position += transform.up * .3f;
        Bullet disparar = lightBall.GetComponent<Bullet>();

        if (sprite.flipX) {
            lightBall.transform.position += transform.right * -.8f;
            disparar.direccion = -1;
        }
        else lightBall.transform.position += transform.right * .8f;
    }

    void InstantiateBullet2() {
        GameObject lightBall2 = Instantiate(prefabBullet2, transform.position, Quaternion.identity);
        lightBall2.transform.position += transform.up * .3f;
        Bullet2 disparar = lightBall2.GetComponent<Bullet2>();

        if (sprite.flipX) {
            lightBall2.transform.position += transform.right * -.8f;
            disparar.direccion = -1;
        }
        else lightBall2.transform.position += transform.right * .8f;
    }
}
