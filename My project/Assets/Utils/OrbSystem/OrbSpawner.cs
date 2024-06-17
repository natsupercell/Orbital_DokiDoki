using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour {
    public KeyCode[] key;
    public ObjectPool pooler;

    public void Awake() {
        this.pooler = GetComponent<ObjectPool>();
    }

    public void spawnOrb(string tag) {
        Resource resource = pooler.spawnFromPool(tag).GetComponent<Resource>();
        if (resource != null) {
            Orb.create(resource, transform);
        }
        else Debug.LogWarning("bug");
    }

    public void Update() {
        if (Input.GetKeyDown(key[0])) {
            spawnOrb("laser");
        }
        if (Input.GetKeyDown(key[1])) {
            spawnOrb("energy");
        }
        if (Input.GetKeyDown(key[2])) {
            spawnOrb("gun");
        }
    }
}
