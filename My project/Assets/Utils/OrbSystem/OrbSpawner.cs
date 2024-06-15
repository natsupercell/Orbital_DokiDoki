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
            /*
            if (resource is Weapon) {
                Debug.Log("weapon");
                Weapon weapon = (Weapon) resource;
                Orb.create(weapon, transform);
            }
            else if (resource is Energy){
                Debug.Log("energy");
                Energy energy = (Energy) resource;
                Orb.create(energy, transform);
            } 
            */
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
