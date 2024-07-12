using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : Utility {
    public GameObject ammoType;
    public string ammoPath; 
    protected AudioManager audioManager;
    protected new AudioClip audio;

    public virtual void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Instantiating methods
    public Weapon(string name, int cost) : base(name) {
        this.cost = cost;
    }

    public Weapon(string name) : this(name, 1) {}

    public override void Activate(GameObject parent) {
        GameObject realParent = parent.transform.parent.gameObject;
        Direction direction = realParent.GetComponent<Movement>().direction;
        GameObject obj = PhotonNetwork.Instantiate(ammoPath, parent.transform.position, direction.toQuaternion());
        AmmoType ammo = obj.GetComponent<AmmoType>();
        ammo.excludeLayer(realParent.GetComponent<Team>().toLayer());
        /* if (audio != null) */ audioManager.PlaySFX(audio);
    }
}
