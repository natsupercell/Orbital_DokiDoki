using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Clone : Consumable {
    public GameObject playerPrefab;
    public Clone() : base("clone", 1) {}

    public override void ReallyActivate(GameObject parent) {
        base.Activate(parent);
        if (PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.Instantiate(
                playerPrefab.name, 
                parent.GetComponent<Rigidbody2D>().position, 
                Quaternion.identity);
        } else {
            Instantiate(
                playerPrefab, 
                parent.GetComponent<Rigidbody2D>().position, 
                Quaternion.identity);
        }
    }
}
