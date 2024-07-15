using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour, AmmoType {
    public float speed = 20f;
    private Rigidbody2D rb;
    private PhotonView view;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.right * speed;
    }
    
    [PunRPC]
    public void excludeLayer(int layer) {
        gameObject.layer = Team.toLayerToBeIgnored(layer);
    }

    public void ExcludeLayerRPC(int layer) {
        view.RPC("excludeLayer", RpcTarget.All, layer);
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
            hitbox.TakeDamageRPC();
        }
        Destroy(gameObject);
    }
}
