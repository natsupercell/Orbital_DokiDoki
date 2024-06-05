using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Reloadable {
    public Laser() : base("laser", 2) {}

    private void Awake() {
        string prefabPath = "LaserBeam";
        ammoType = Resources.Load<GameObject>(prefabPath);
    }
}
