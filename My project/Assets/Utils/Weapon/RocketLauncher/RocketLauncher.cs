using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon {
    public RocketLauncher() : base("rocket launcher", 3) {}

    public override void Awake() {
        base.Awake();
        string prefabPath = "AmmoTypes/Rocket";
        ammoType = Resources.Load<GameObject>(prefabPath);
        audio = audioManager.shootRocket;
    }
}
