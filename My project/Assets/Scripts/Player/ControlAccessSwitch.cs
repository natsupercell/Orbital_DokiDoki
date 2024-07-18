using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControlAccessSwitch : MonoBehaviour {
    public new bool enabled;
    private PhotonView view;

    private void Start() {
        enabled = true;
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void enable() {
        enabled = true;
    }

    [PunRPC]
    private void disable() {
        enabled = false;
        GetComponent<Movement>().Stop();
    }

    public void EnableRPC() {
        view.RPC("enable", RpcTarget.All);
    }

    public void DisableRPC() {
        view.RPC("disable", RpcTarget.All);
    }
}
