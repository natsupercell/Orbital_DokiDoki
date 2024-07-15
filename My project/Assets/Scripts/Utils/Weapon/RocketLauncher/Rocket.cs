using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocket : MonoBehaviour, AmmoType {
    public float speed = 9f;
    public Rigidbody2D rb;
    public GameObject damagingArea;
    public string ammoPath;
    private AudioManager audioManager;
    private new AudioClip audio;
    private PhotonView view;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.right * speed;
        ammoPath = "AmmoTypes/RocketExplosiveArea";
        damagingArea = Resources.Load<GameObject>(ammoPath);

        // optimise this
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        audio = audioManager.rocketExplode;
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
        // Implement logic for when the rocket hits something
        Debug.Log("hit");
        audioManager.PlaySFX(audio);
        //StartCoroutine(Explode());
        PhotonNetwork.Instantiate(ammoPath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /*
    private IEnumerator Explode() {
        Instantiate(damagingArea, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }
    */
}
