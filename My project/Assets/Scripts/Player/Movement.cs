using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public PhotonView view;
    private ControlAccessSwitch control;
    public Direction direction = Direction.RIGHT;
    private Vector2 movement;
    private bool isMoving = false;

    [SerializeField]
    public KeyCode[] moveKeys = new KeyCode[4];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        control = GetComponent<ControlAccessSwitch>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        Physics2D.gravity = Vector2.zero;
    }

    public virtual void Update() {
        if (view.IsMine) {
            if (control.enabled) {
                // Check for individual key presses to prevent diagonal movement
                if (Input.GetKeyDown(moveKeys[0])) GoLeft();
                else if (Input.GetKeyDown(moveKeys[1])) GoRight();
                else if (Input.GetKeyDown(moveKeys[2])) GoUp();
                else if (Input.GetKeyDown(moveKeys[3])) GoDown();
                if (movement == Vector2.zero
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
    }

    private void GoLeft() {
        movement = Vector2.left;
        direction = Direction.LEFT;
        transform.rotation = direction.toQuaternion();
        if (!isMoving) { 
            isMoving = true;
            UpdateRPC();
        }
        
    }

    private void GoRight() {
        movement = Vector2.right;
        direction = Direction.RIGHT;
        transform.rotation = direction.toQuaternion();
        if (!isMoving) { 
            UpdateRPC();
        }
    }

    private void GoUp() {
        movement = Vector2.up;
        direction = Direction.UP;
        transform.rotation = direction.toQuaternion();
        if (!isMoving) { 
            UpdateRPC();
        }
    }

    private void GoDown() {
        movement = Vector2.down;
        direction = Direction.DOWN;
        transform.rotation = direction.toQuaternion();
        if (!isMoving) { 
            UpdateRPC();
        }
    }

    public void Stop() {
        movement = Vector2.zero;
        if (isMoving) { 
            UpdateRPC();
        }
    }

    void FixedUpdate() {
        // Move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    [PunRPC]
    void UpdateAnimations() {
        animator.SetBool("IsMoving", isMoving);
        if (!isMoving)
        {
            // Play idle animation 
            animator.Play("Idle");
        }
        else
        {
            // Play walking animation
            animator.Play("Move");
        }
    }

    [PunRPC]
    void ChangeMovingState() {
        isMoving = !isMoving;
    }

    void UpdateRPC() {
        view.RPC("ChangeMovingState", RpcTarget.All);
        view.RPC("UpdateAnimations", RpcTarget.All);
    }
}
