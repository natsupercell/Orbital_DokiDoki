using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Laser : Weapon {
    public Laser() : base("laser", 2) {}

    public override void Awake() {
        base.Awake();
        ammoPath = "AmmoTypes/LaserBeam";
        ammoType = Resources.Load<GameObject>(ammoPath);
        audio = audioManager.laser;
    }
}
