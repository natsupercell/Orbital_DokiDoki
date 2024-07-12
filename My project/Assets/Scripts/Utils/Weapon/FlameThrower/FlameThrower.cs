using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon {
    public FlameThrower() : base("flame thrower", 1) {}

    public override void Awake() {
        base.Awake();
        ammoPath = "AmmoTypes/Flame";
        ammoType = Resources.Load<GameObject>(ammoPath);
        audio = audioManager.flame;
    }
}
