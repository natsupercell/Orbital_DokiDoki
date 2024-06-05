using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Reloadable {
    public Gun() : base("gun", 2) {}

    private void Awake() {
        string prefabPath = "Bullet";
        ammoType = Resources.Load<GameObject>(prefabPath);
    }
}
