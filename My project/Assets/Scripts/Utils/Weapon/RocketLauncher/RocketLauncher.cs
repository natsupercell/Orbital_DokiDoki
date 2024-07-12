using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketLauncher : Weapon {
    public RocketLauncher() : base("rocket launcher", 3) {}

    public override void Awake() {
        base.Awake();
        ammoPath = "AmmoTypes/Rocket";
        ammoType = Resources.Load<GameObject>(ammoPath);
        audio = audioManager.shootRocket;
    }
}
