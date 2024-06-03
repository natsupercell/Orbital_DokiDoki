using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update() {
        if (transform.position.x < -50 || transform.position.x > 50
         || transform.position.y < -50 || transform.position.y > 50) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo) {
        Debug.Log("hit " + hitInfo.ToString());
        // Implement logic for when the laser hits something
        Hitbox hitbox = hitInfo.GetComponent<Hitbox>();
        if (hitbox != null) {
            hitbox.takeDamage();
        }
        Destroy(gameObject);
    }
}
