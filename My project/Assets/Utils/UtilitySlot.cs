using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UtilitySlot : MonoBehaviour {
    public Utility utility;
    public KeyCode key;
    public PhotonView view;
    void Start() {

        view = GetComponent<PhotonView>();
    }
    
    public void Update() {
        if (view.IsMine && Input.GetKeyDown(key)) {
            utility.activate(gameObject);
            if (utility.destroy()) {
                utility = null;
            }
        }
    }
}
