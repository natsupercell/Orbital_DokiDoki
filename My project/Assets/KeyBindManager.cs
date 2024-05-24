using UnityEngine;
using System;
using System.Collections.Generic;

public class KeybindManager : MonoBehaviour {
    private List<Keybind> keybinds = new List<Keybind>();

    public void RemoveKeybind(string actionName) {
        keybinds.RemoveAll(k => k.actionName == actionName);
    }

    public void UpdateKeybind(string actionName, KeyCode keyCode) {
        Keybind keybind = keybinds.Find(k => k.actionName == actionName);
        if (keybind != null) {
            keybind.keyCode = keyCode;
        } else {
            keybinds.Add(new Keybind(actionName, keyCode));
        }
    }

    public Keybind Find(string actionName) {
        return keybinds.Find(k => k.actionName == actionName);
    }

    void Start() {

    }

    void Update() {
        
    }
}