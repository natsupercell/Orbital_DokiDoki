using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Photon.Pun;

public class MovementClone : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public PhotonView view;

    private Direction direction = Direction.RIGHT;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement = Vector2.left;
                direction = Direction.LEFT;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movement = Vector2.right;
                direction = Direction.RIGHT;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                movement = Vector2.up;
                direction = Direction.UP;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                movement = Vector2.down;
                direction = Direction.DOWN;
            }

            // Update animations
            UpdateAnimations();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    void UpdateAnimations()
    {
        float speed = movement.sqrMagnitude;
        bool isMoving = speed > 0.00001f;
        
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsMoving", isMoving);

        if (!isMoving)
        {
            // Play idle animation based on the facing direction
            switch (direction)
            {
                case Direction.LEFT:
                    animator.Play("IdleLeft");
                    break;
                case Direction.RIGHT:
                    animator.Play("IdleRight");
                    break;
                case Direction.UP:
                    animator.Play("IdleUp");
                    break;
                case Direction.DOWN:
                    animator.Play("IdleDown");
                    break;
            }
        }
        else
        {
            // Play walking animation
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }
}
