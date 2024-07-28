using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TileHitbox : Hitbox {
    [PunRPC]
    private void TakeDamage() {
        gameObject.GetComponent<PhotonCustomControl>().DisableRPC();
    }

    public override void TakeDamageRPC() {
        view.RPC("TakeDamage", RpcTarget.All);
    }
}
