using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Clone : Consumable {
    public GameObject playerPrefab;
    public Clone() : base("clone", 1) {}

    public override void activate(GameObject parent) {
        base.activate(parent);
        if (PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.Instantiate(
                playerPrefab.name, 
                parent.GetComponent<Rigidbody2D>().position + new Vector2(1,1), 
                Quaternion.identity);
        } else {
            Instantiate(
                playerPrefab, 
                parent.GetComponent<Rigidbody2D>().position + new Vector2(1,1), 
                Quaternion.identity);
        }
    }
}
