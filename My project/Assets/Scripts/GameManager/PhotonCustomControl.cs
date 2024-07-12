using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonCustomControl : MonoBehaviourPun {
    [PunRPC]
    void Enable() {
        gameObject.SetActive(true);
    }

    [PunRPC]
    void Disable() {
        gameObject.SetActive(false);
    }

    [PunRPC]
    void Move(Vector3 position) {
        transform.position = position;
    }

    [PunRPC]
    void Move(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }

    [PunRPC]
    public void SetParent(int childViewID, int parentViewID, bool worldPositionStays) {
        PhotonView childPhotonView = PhotonView.Find(childViewID);
        PhotonView parentPhotonView = PhotonView.Find(parentViewID);

        if (childPhotonView != null) {
            Transform childTransform = childPhotonView.transform;
            Transform parentTransform = parentPhotonView != null ? parentPhotonView.transform : null;
            childTransform.SetParent(parentTransform, worldPositionStays);
            Debug.Log($"Set {childTransform.name}'s parent to {parentTransform?.name ?? "null"}");
        }
        else {
            Debug.LogError("Child PhotonView not found.");
        }
    }

    public void EnableRPC() {
        photonView.RPC("Enable", RpcTarget.All);
    }

    public void DisableRPC() {
        photonView.RPC("Disable", RpcTarget.All);
    }

    public void MoveRPC(Vector3 position) {
        photonView.RPC("Move", RpcTarget.All, position);
    }

    public void MoveRPC(Vector3 position, Quaternion rotation) {
        photonView.RPC("Move", RpcTarget.All, position, rotation);
    }

    public void SetParentRPC(GameObject parent, bool worldPositionStays) {
        int childViewID = GetComponent<PhotonView>().ViewID;
        int parentViewID = parent != null ? parent.GetComponent<PhotonView>().ViewID : 0; // 0 means no parent (root)
        photonView.RPC("SetParent", RpcTarget.All, childViewID, parentViewID, worldPositionStays);
    }

    public void SetParentRPC(GameObject parent) {
        SetParentRPC(parent, true);
    }
}
