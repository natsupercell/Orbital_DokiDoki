using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour {
    public KeyCode[] key;
    public ObjectPool pooler;

    /* For debugging */ public float INCREMENT = 0.1f; public int SCALE = 0; 

    public void Awake() {
        this.pooler = GetComponent<ObjectPool>();
    }

    public void spawnOrb(string tag) {
        Resource resource = pooler.spawnFromPool(tag).GetComponent<Resource>();
        GameObject orb = pooler.spawnFromPool("orb");

        // Instantiating the orb
        if (resource != null) {
            orb.transform.position = transform.position + new Vector3(INCREMENT * (SCALE % 2),0f,-10f); SCALE++;
            orb.GetComponent<Orb>().content = resource;

            Debug.Log("Created a new " + resource + " orb");
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
