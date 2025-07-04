using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : SpellWithDelay {
    public Teleport() : base("teleport") {}
    public override void castSpell(GameObject parent) {
        parent.transform.position = crosshair.transform.position;
    }
}
