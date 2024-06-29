using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour, Team {
    public static int count = 0;
    public static int layerID = 8;
    public bool status;

    public void Start() {
        alive();
        // Debug.Log("New ally");
    }

    public void alive() {
        if (!status) count++;
        status = true;
    }

    public void died() {
        if (status) count--;
        status = false;
    }

    public int toLayer() {
        return layerID;
    }

    public static bool isEliminated() {
        return count == 0;
    }
}
