using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draco : MonoBehaviour {
    public int hp;
    public float spd;
    public bool CanMove;
    
    Transform left;
    Transform right;

    void Start() {
        GetComponents();

        anim.SetInteger("HP", hp);

        left = transform.FindChild("Left");
        right = transform.FindChild("Right");
    }

    public Vector2 movement = new Vector2(-1, 0);
    public bool alive; //lo activa el boss manager
    bool aliveSound;
    public void Move() {
        if (CanMove) {
            rb.velocity = movement * spd;
            anim.Play("Move");
            anim.SetFloat("FlyY", rb.velocity.y);
            if (!AudSrc.isPlaying) AudSrc.PlayOneShot(GameObject.Find("Audio").GetComponent<AudioManager>().GetSFX(11));

            if (aliveSound) return;
            AudSrc.PlayOneShot(GameObject.Find("Audio").GetComponent<AudioManager>().GetSFX(7));
            aliveSound = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.waypointsDraco) {
            if (lvlManager.lvlOnScene == 2 && !cambiaSentido || lvlManager.lvlOnScene == 3) {
                transform.right = info.gameObject.transform.right;
                switch (info.gameObject.tag) {
                    case "subir": movement = new Vector2(0, 1); break;
                    case "bajar": movement = new Vector2(0,-1); break;
                    case "izq":   movement = new Vector2(-1,0); break;
                    case "der":   movement = new Vector2(1, 0); break;
                }
            }

            else { 
                transform.right = info.gameObject.transform.right * -1;
                switch (info.gameObject.tag) {
                    case "subir": movement = new Vector2(0,-1); break;
                    case "bajar": movement = new Vector2(0, 1); break;
                    case "izq":   movement = new Vector2(1, 0); break;
                    case "der":   movement = new Vector2(-1,0); break;
                }
            }
        }
    }

    void Update() {
        Stomp();
        DisableCheck();
        Disable_Head_Body(); //desabilito la cabeza cuando vuela horizontal, porque sinó la podés hitear quedándote parado...

        if (lvlManager.lvlOnScene == 3 && bossManager.ActivateBoss) alive = true;
    }

    public bool Stomped; //lo activan los hijos 'head' y 'body' al ser triggereados
    void Stomp() {
        if (Stomped) {
            Hit();
            ChangeDirection();
            IncreaseSpd();
            Stomped = false;
        }
    }

    void Disable_Head_Body() {
        if (alive) {
            if (rb.velocity.y == 0) {
                transform.Find("Head").gameObject.SetActive(false);
                transform.Find("Body").gameObject.SetActive(true);
            }
            else {
                transform.Find("Head").gameObject.SetActive(true);
                transform.Find("Body").gameObject.SetActive(false);
            }
        }
        
    }

    public GameObject explosion;
    void Death() {
        if (hp <= 0) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            lvlManager.monstruosRestantes--;
            switch (lvlManager.lvlOnScene) {
                case 2: DropItems(); break;
                case 3: if (lvlManager.monstruosRestantes == -6) DropItems(); break;
            }
            Destroy(gameObject);
        }
    }   

    void Hit() {
        AudSrc.PlayOneShot(GameObject.Find("Audio").GetComponent<AudioManager>().GetSFX(8));
        hp--;
        anim.Play("Hit");
        anim.SetInteger("HP", hp);
        Disabled();
        if (hp <= 0) Death();
    }

    void Disabled() {
        CanMove = false;
        rb.velocity = Vector2.zero;
        disableTime = 0;
    }

    float disableTime;
    void DisableCheck() {
        disableTime += Time.deltaTime;
        if (disableTime > .2 && alive) CanMove = true;
    }

    bool cambiaSentido;
    void ChangeDirection() {
        movement = movement * -1;
        transform.right = transform.right * -1;
        if (!cambiaSentido) cambiaSentido = true;
        else cambiaSentido = false;
    }

    void IncreaseSpd() { spd = spd + .7f; }

    public GameObject dropHp;
    public GameObject dropSkill;
    void DropItems() {
        Instantiate(dropHp, left.position, Quaternion.identity);
        Instantiate(dropSkill, right.position, Quaternion.identity);
    }

    public Rigidbody2D rb;
    public Animator anim;
    public LevelManager lvlManager;
    public BossManager bossManager;
    public SpriteRenderer sprite;
    public BoxCollider2D m_collider; //si alive = true, el cerebro lo habilita
    AudioSource AudSrc;
    void GetComponents() {
        bossManager = GameObject.Find("Managers").GetComponent<BossManager>();
        lvlManager  = GameObject.Find("Managers").GetComponent<LevelManager>();
        rb         = GetComponent<Rigidbody2D>();
        sprite     = GetComponent<SpriteRenderer>();
        anim       = GetComponent<Animator>();
        AudSrc = GetComponent<AudioSource>();
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.enabled = false;
        transform.Find("Head").gameObject.SetActive(false);
        transform.Find("Body").gameObject.SetActive(false);
    }
}