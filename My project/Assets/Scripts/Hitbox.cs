using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public bool shielded;
    public Team team;

    public void Awake() {
        team = GetComponent<Team>();
    }
    public void takeDamage() {
        if (shielded) shielded = false;
        else {
            team.died();
            gameObject.SetActive(false);
        }
    }
}
