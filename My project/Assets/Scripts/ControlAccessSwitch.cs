using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAccessSwitch : MonoBehaviour {
    public new bool enabled;

    private void Start() {
        enabled = false;
    }

    public void disable() {
        enabled = false;
        GetComponent<Movement>().Stop();
    }
}
