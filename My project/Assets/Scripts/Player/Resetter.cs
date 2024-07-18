using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    public void reset() {
        GetComponent<PhotonCustomControl>().EnableRPC();
        GetComponent<Team>().AliveRPC();
        GetComponent<ControlAccessSwitch>().DisableRPC();
    }
}
