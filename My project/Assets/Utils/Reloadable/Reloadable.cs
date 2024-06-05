using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloadable : Utility {
    // Ammo's type and number of rounds available
    public GameObject ammoType;
    public int ammoCount;
    public Vector3 debugScaler;
    

    // Instantiating methods
    public Reloadable(string name, int ammoCount) : base(name) {
        this.ammoCount = ammoCount;
    }

    public Reloadable(string name) : this(name, 1) {}

    public void reload(int count) {
        ammoCount += count;
    }

    // Activating, reducing number of ammo by 1
    public override void activate(GameObject parent) {
        if (ammoCount == 0) {
            Debug.Log("Out of ammo!");
        } else {
            Direction direction = parent.GetComponent<Movement>().direction;
            Instantiate(ammoType, parent.transform.position + Vector3.Scale(debugScaler, direction.toVector3()), direction.toQuaternion());
            ammoCount--;
        }
    }
}
