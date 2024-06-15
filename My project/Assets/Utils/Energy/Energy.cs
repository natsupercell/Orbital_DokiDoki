using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour, Resource {
    public int value;

    public Energy(int value) {
        this.value = value;
    }

    public Energy() : this(2) {}

    public int getValue() {
        Destroy(this);
        return value;
    }

    public void set(int value) {
        this.value = value;
    }
}
