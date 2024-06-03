using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public void takeDamage() {
        Destroy(gameObject);
    }
}
