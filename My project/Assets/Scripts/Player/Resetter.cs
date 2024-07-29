using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {
    public void reset() {
        Inventory inventory = transform.GetChild(0).gameObject.GetComponent<Inventory>();
        inventory.energy += 30;
        inventory.Reset();
        GetComponent<Hitbox>().DisableInvincibilityRPC();
        GetComponent<PhotonCustomControl>().EnableRPC();
        GetComponent<Team>().AliveRPC();
        GetComponent<ControlAccessSwitch>().DisableRPC();
    }
}
