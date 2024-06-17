using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    private Team team;

    public void Awake() {
        team = GetComponent<Team>();
    }
    public void reset() {
        gameObject.SetActive(true);
        team.alive();
    }
}
