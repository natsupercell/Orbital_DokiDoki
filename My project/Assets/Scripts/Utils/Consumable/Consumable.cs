using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Utility {
    // Number of unused charges
    public int charges;

    // Instantiating
    public Consumable(string name, int charges) : base(name) {
        this.charges = charges;
    }

    // Quick instantiating, default number of charges is 1
    public Consumable(string name) : this(name, 1) {}

    // Activating, reducing number of charges by 1
    public override void Activate(GameObject parent) {
        if (charges <= 0) Debug.Log("No more charges!");
        else { 
            charges--;
            ReallyActivate(parent);
        }
    }

    public virtual void ReallyActivate(GameObject parent) {}
}
