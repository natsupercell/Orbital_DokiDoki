using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CrosshairControl : MonoBehaviour {
    public float speed = 3f;
    public Rigidbody2D rb;
    private PhotonView view;
    private Vector2 movement;

    [SerializeField]
    public KeyCode[] moveKeys = new KeyCode[4];

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    void OnEnable() {
        Debug.Log("Crosshair activated");
        Stop();
    }

    public virtual void Update() {
        if (view.IsMine) {
            // Check for individual key presses to prevent diagonal movement
            if (Input.GetKeyDown(moveKeys[0])) GoLeft();
            else if (Input.GetKeyDown(moveKeys[1])) GoRight();
            else if (Input.GetKeyDown(moveKeys[2])) GoUp();
            else if (Input.GetKeyDown(moveKeys[3])) GoDown();
            else if (movement == Vector2.zero
                    || (Input.GetKeyUp(moveKeys[0]) && movement == Vector2.left)
                    || (Input.GetKeyUp(moveKeys[1]) && movement == Vector2.right)
                    || (Input.GetKeyUp(moveKeys[2]) && movement == Vector2.up)
                    || (Input.GetKeyUp(moveKeys[3]) && movement == Vector2.down)) {
                if (Input.GetKey(moveKeys[0])) GoLeft();
                else if (Input.GetKey(moveKeys[1])) GoRight();
                else if (Input.GetKey(moveKeys[2])) GoUp();
                else if (Input.GetKey(moveKeys[3])) GoDown();
                else Stop();
            }
        }
    }

    private void GoLeft() {
        movement = Vector2.left;
    }

    private void GoRight() {
        movement = Vector2.right;
    }

    private void GoUp() {
        movement = Vector2.up;
    }

    private void GoDown() {
        movement = Vector2.down;
    }

    public void Stop() {
        movement = Vector2.zero;
    }

    void FixedUpdate() {
        // Move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
