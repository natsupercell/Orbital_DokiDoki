using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon {
    public Laser() : base("laser", 2) {}

    private void Awake() {
        string prefabPath = "LaserBeam";
        ammoType = Resources.Load<GameObject>(prefabPath);
    }

    public static Utility create() {
        return new Laser();
    }
}
