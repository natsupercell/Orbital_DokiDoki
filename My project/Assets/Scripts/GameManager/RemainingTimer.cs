using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RemainingTimer : MonoBehaviour {
    public float remainingTime;
    public float startingTime;
    private PhotonView view;

    void Awake() {
        view = GetComponent<PhotonView>();
    }

    public RemainingTimer(float startingTime) {
        this.startingTime = startingTime;
        Reset();
    }
    
    void Update() {
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        }
        else {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);     
        // timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    [PunRPC]
    public void Reset() {
        remainingTime = startingTime;
    }
    
    [PunRPC]
    public void Set(float time) {
        remainingTime = time;
    }

    public void ResetRPC() {
        view.RPC("Reset", RpcTarget.All);
    }
    
    public void SetRPC(float time) {
        view.RPC("Set", RpcTarget.All, time);
    }
}
