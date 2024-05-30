using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OldPlayer : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private PhotonView view;

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        Physics2D.gravity = Vector2.zero;
    }

    void Update()
    {
        if (view.IsMine)
        {
            // Reset movement to zero
            movement = Vector2.zero;

            // Check for individual key presses to prevent diagonal movement
            if (Input.GetKey(KeyCode.A))
            {
                movement = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                movement = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                movement = Vector2.down;
            }

            // Apply speed to the movement vector
            movement *= speed;

            // Update animator parameters
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            // Move the player
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }
}