using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private bool isDead = false;
    private bool onFloor = true;

    private Rigidbody2D rb2d;
    private Animator anim;

    public float jumpForce = 200f;

    public GameObject kunaiPrefab;
    public float cooldownTime;

    private float nextThrowTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {

            // saltar
            if(onFloor) {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    onFloor = false;
                    rb2d.AddForce(new Vector2(0, jumpForce));
                    anim.SetBool("Jumping", true);
                }
            }

            // movimento
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            switch(Input.GetAxisRaw("Horizontal")) {
                case -1:
                rb2d.velocity = new Vector2(-8.0f, rb2d.velocity.y);
                break;

                case 1:
                rb2d.velocity = new Vector2(8.0f, rb2d.velocity.y);
                break;

                default:
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                break;
            }

            // shooting
            if (Input.GetKeyDown("space") && Time.time > nextThrowTime)
            {
                anim.SetTrigger("Throw");
                Shoot();

                nextThrowTime = Time.time + cooldownTime;
            }
        }
        else {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            onFloor = true;
            anim.SetBool("Jumping", false);
        }
        else if(other.gameObject.CompareTag("Obstacle")) {
            rb2d.velocity = Vector2.zero;
            GameControl.instance.PlayerLost();
            isDead = true;
            anim.SetTrigger("Die");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Obstacle")) {
            rb2d.velocity = Vector2.zero;
            GameControl.instance.PlayerLost();
            isDead = true;
            anim.SetTrigger("Die");
        }
    }

    public void Shoot() {
        GameObject k = Instantiate(kunaiPrefab) as GameObject;
        k.transform.position = transform.position;
    }
}
