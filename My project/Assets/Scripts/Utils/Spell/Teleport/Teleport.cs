using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Spell {
    public Teleport() : base("teleport") {}
    public override void castSpell(GameObject parent) {
        GameObject realParent = parent.transform.parent.gameObject;
        realParent.GetComponent<PhotonCustomControl>().MoveRPC(crosshair.transform.position);
    }
}
