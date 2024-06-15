using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour, Resource {
    // Utility's name
    public new string name;
    /*
     * Check if the utility has no more charges
     * (Side note) Reusable.empty is always false 
     */
    private bool empty;

    // Instantiating
    public Utility(string name) {
        this.name = name;
        empty = false;
    }

    // ranOut() change an utility's state to empty
    public void ranOut() {
        empty = true;
    }

    /*
     * If destroy() returns true, the UtilitySlot holding it will be
     * converted to an empty slot
     */
    public bool destroy() {
        return empty;
    }

    // Activating 
    public virtual void activate(GameObject parent) {}
    public virtual void deactivate(GameObject parent) {}
}
