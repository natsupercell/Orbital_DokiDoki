using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour {
    private Rigidbody2D body;
    private PhotonView view;
    private float moveSpeed;
    //private PlayerKeyBindManager playerKeyBindManager = new PlayerKeyBindManager();

    void Start() {
        body = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        moveSpeed = 10F;
        //playerKeyBindManager.Initialize();
        Physics2D.gravity = Vector2.zero;
    }

    void FixedUpdate() {
        if(view.IsMine) {
            Vector2 movement = Vector2.zero;

            //if (Input.GetKey(playerKeyBindManager.moveLeft.getKey())) {
            if (Input.GetKey(KeyCode.A)) {
                movement = Vector2.left * moveSpeed;
            } 

            //else if (Input.GetKey(playerKeyBindManager.moveRight.getKey())) {
            else if (Input.GetKey(KeyCode.D)) {
                movement = Vector2.right * moveSpeed;
            }

            //else if (Input.GetKey(playerKeyBindManager.moveUp.getKey())) {
            else if (Input.GetKey(KeyCode.W)) {
                movement = Vector2.up * moveSpeed;
            }

            //else if (Input.GetKey(playerKeyBindManager.moveDown.getKey())) {
            else if (Input.GetKey(KeyCode.S)) {
                movement = Vector2.down * moveSpeed;
            }

            body.MovePosition(new Vector2(body.position.x + movement.x * Time.deltaTime,
                    body.position.y + movement.y * Time.deltaTime));
        }
    }
}
