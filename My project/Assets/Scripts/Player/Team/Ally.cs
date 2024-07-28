using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ally : MonoBehaviour, Team {
    private static int count = 0;
    private static int layerID = 8;
    private bool status = false;
    private PhotonView view;

    public void Awake() {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Alive() {
        if (!status) count++;
        status = true;
    }

    [PunRPC]
    public void Died() {
        if (status) count--;
        status = false;
    }

    public static bool IsEliminated() {
        /*
            Debug.Log(count + " ally"); 
            return false;
        */
        return count == 0;
    }

    public static void Reset() {
        count = 0;
    }

    public int toLayer() {
        return layerID;
    }


    public void AliveRPC() {
        view.RPC("Alive", RpcTarget.MasterClient);
    }

    public void DiedRPC() {
        view.RPC("Died", RpcTarget.MasterClient);
    }
}
