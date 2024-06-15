using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Utility {
    // Ammo's type and number of rounds available
    public GameObject ammoType;
    public int cost;
    public static Vector3 debugScaler = new Vector3(0.33f, 0.33f, 0.33f);

    // Instantiating methods
    public Weapon(string name, int cost) : base(name) {
        this.cost = cost;
    }

    public Weapon(string name) : this(name, 1) {}

    // Activating, reducing number of ammo by 1
    public override void activate(GameObject parent) {
        Direction direction = parent.GetComponent<Movement>().direction;
        Instantiate(ammoType,
        parent.transform.position 
        + Vector3.Scale(debugScaler, direction.toVector3()), 
        direction.toQuaternion());
    }
}
