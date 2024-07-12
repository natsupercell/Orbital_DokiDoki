using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour, Resource {
    public int value;

    public Energy(int value) {
        this.value = value;
    }

    public Energy() : this(2) {}

    public string getName() {
        return "energy";
    }

    public int getValue() {
        GetComponent<PhotonCustomControl>().DisableRPC();
        return value;
    }

    public void set(int value) {
        this.value = value;
    }
}
