using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private PolygonCollider2D col;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        col = GetComponent<PolygonCollider2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Weapon")) {
            isDead = true;
            anim.SetTrigger("Die");
            col.enabled = false;
        }
    }
}
