using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, AmmoType {
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    public void excludeLayer(int layer) {
        gameObject.layer = Team.toLayerToBeIgnored(layer);
    }

    void Update() {
        if (transform.position.x < -50 || transform.position.x > 50
         || transform.position.y < -50 || transform.position.y > 50) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo) {
        // Implement logic for when the bullet hits something
        Hitbox hitbox = hitInfo.GetComponent<Hitbox>();
        if (hitbox != null) {
            hitbox.takeDamage();
        }
        Destroy(gameObject);
    }
}
