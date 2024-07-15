using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OrbSpawner : MonoBehaviourPunCallbacks {
    public KeyCode[] key;
    public ObjectPool pooler;
    public PhotonView view;

    public void Awake() {
        this.pooler = GetComponent<ObjectPool>();
        this.view = GetComponent<PhotonView>();
    }

    public void SpawnOrb(string tag) {
        GameObject resource = pooler.SpawnFromPool(tag);
        GameObject orb = pooler.SpawnFromPool("Orb");

        // Instantiating the orb
        if (resource != null) {
            orb.GetComponent<PhotonCustomControl>().MoveRPC(transform.position + new Vector3(0f,0f,-10f));
            orb.GetComponent<Orb>().Add(resource);

            Debug.Log("Created a new " + resource.GetComponent<Resource>().getName() + " orb");
        }
        /*
        if (resource != null) {
            Orb.create(resource, transform);
        }
        */

        else Debug.LogWarning("bug");
    }

    public void Update() {
        if (Input.GetKeyDown(key[0])) {
            SpawnOrb("Shield");
        }
        if (Input.GetKeyDown(key[1])) {
            SpawnOrb("Gun");
        }
        if (Input.GetKeyDown(key[2])) {
            SpawnOrb("Laser");            
        }
        if (Input.GetKeyDown(key[3])) {
            SpawnOrb("Rocket Launcher");
        }
        if (Input.GetKeyDown(key[4])) {
            SpawnOrb("Flame Thrower");
        }
        if (Input.GetKeyDown(key[5])) {
            SpawnOrb("Energy");
        }
    }
}
