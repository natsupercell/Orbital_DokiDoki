using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Consumable {
    public GameObject bullet;
    public Gun() : base("gun", 2) {}

    public override void activate(GameObject parent) {
        base.activate(parent);
        Shoot(parent);
    }

    private void Shoot(GameObject parent) {
        Direction direction = parent.GetComponent<Movement>().direction;
        Instantiate(bullet, parent.transform.position + direction.toVector3(), direction.toQuaternion());
    }
}
