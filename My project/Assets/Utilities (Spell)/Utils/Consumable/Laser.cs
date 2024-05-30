using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Consumable {
    public Laser() : base("laser", 2) {}

    public override void activate(GameObject parent) {
        base.activate(parent);
        Debug.Log("hihi");
    }
}
