using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Lightning : Spell {
    private GameObject bolt;
    private string ammoPath;
    public Lightning() : base("lightning") {}

    public override void Awake() {
        base.Awake();
        ammoPath = "AmmoTypes/LightningBolt";
        bolt = Resources.Load<GameObject>(ammoPath);
    }

    public override void castSpell(GameObject parent) {
        PhotonNetwork.Instantiate(ammoPath, crosshair.transform.position, Quaternion.identity);
    }
}
