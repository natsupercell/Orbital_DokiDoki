using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public Gun() : base("gun", 1) {}

    public override void Awake() {      
        base.Awake();
        string prefabPath = "Bullet";
        ammoType = Resources.Load<GameObject>(prefabPath);
        audio = audioManager.shooting;
    }
}
