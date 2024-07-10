using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour, Resource {
    // Utility's name
    public new string name;
    /*
     * Cost of using this weapon
     * (Side note) NonWeapon.cost is always 0 
     */
    public int cost;

    // Instantiating
    public Utility(string name) {
        this.name = name;
        cost = 0;
    }

    public string getName() {
        return name;
    }

    // Activating 
    public virtual void Activate(GameObject parent) {}
    public virtual void Deactivate(GameObject parent) {}
}
