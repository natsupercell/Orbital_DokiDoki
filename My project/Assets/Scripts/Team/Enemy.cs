using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Team {
    public static int count = 0;
    public static int layerID = 9;
    public bool status;

    public void Start() {
        alive();
        // Debug.Log("New enemy");
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
