using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairControl : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 movement;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        Debug.Log("Crosshair activated");
    }

    void Update() {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) {
            movement = Vector2.left;
        } else if (Input.GetKey(KeyCode.D)) {
            movement = Vector2.right;
        } else if (Input.GetKey(KeyCode.W)) {
            movement = Vector2.up;
        } else if (Input.GetKey(KeyCode.S)) {
            movement = Vector2.down;
        }
    }
    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
