using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UtilitySlot : MonoBehaviour {
    public Utility[] utility = new Utility[3];
    public KeyCode[] key = new KeyCode[3];
    public PhotonView view;
    void Start() {
        view = GetComponent<PhotonView>();
        //key = new KeyCode[3];
    }

    public void activate(int i) {
        utility[i].activate(gameObject);
        if (utility[i].destroy()) {
            utility[i] = null;
        }
    }
    
    public void Update() {
        if (view.IsMine) {
            for (int i = 0; i < 3; i++) if (Input.GetKeyDown(key[i])) {
                activate(i);
            }
        }
    }
}
