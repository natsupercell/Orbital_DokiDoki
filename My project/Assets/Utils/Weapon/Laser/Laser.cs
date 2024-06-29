using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Laser : Weapon {
    public Laser() : base("laser", 2) {}

    public override void Awake() {
        base.Awake();
        string prefabPath = "LaserBeam";
        ammoType = Resources.Load<GameObject>(prefabPath);
        audio = audioManager.laser;
    }
}
