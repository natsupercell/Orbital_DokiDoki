using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public Gun() : base("gun", 1) {}

    private void Awake() {
        string prefabPath = "Bullet";
        ammoType = Resources.Load<GameObject>(prefabPath);
    }
}
