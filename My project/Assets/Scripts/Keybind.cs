using UnityEngine;
using System;
using System.Collections.Generic;

public class Keybind {
    public string actionName;
    public KeyCode keyCode;
    public Keybind(string actionName, KeyCode keyCode) {
        this.actionName = actionName;
        this.keyCode = keyCode;
    }
    public KeyCode getKey() {
        return keyCode;
    }
}