using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Utility {
    public GameObject ammoType;
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
    public override void activate(GameObject parent)
    {
        Direction direction = parent.GetComponent<Movement>().direction;
        GameObject obj = Instantiate(ammoType, parent.transform.position, direction.toQuaternion());
        AmmoType ammo = obj.GetComponent<AmmoType>();
        ammo.excludeLayer(parent.GetComponent<Team>().toLayer());
        audioManager.PlaySFX(audio);
    }
}
