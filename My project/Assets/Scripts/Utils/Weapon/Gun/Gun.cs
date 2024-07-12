using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public Gun() : base("gun", 1) {}

    public override void Awake() {      
        base.Awake();
        ammoPath = "AmmoTypes/Bullet";
        ammoType = Resources.Load<GameObject>(ammoPath);
        audio = audioManager.shooting;
    }
}
