using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    public void reset() {
        gameObject.SetActive(true);
        GetComponent<Team>().alive();
        GetComponent<ControlAccessSwitch>().enabled = false;
    }
}
