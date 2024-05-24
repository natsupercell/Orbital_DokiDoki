using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBindManager {
    public Keybind moveLeft;
    public Keybind moveRight;
    public Keybind moveUp;
    public Keybind moveDown;

    public void Initialize() {
        moveLeft = new Keybind("moveLeft", KeyCode.A);
        moveRight = new Keybind("moveRight", KeyCode.D);
        moveUp = new Keybind("moveUp", KeyCode.W);
        moveDown = new Keybind("moveDown", KeyCode.S);
    }
}

