using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks {
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField sideSelect;
    public TMP_InputField playerSelect;
    private GameObject playerData;

    void Awake() {
        playerData = GameObject.FindGameObjectsWithTag("Data")[0];
        DontDestroyOnLoad(playerData);
    }

    public void CreateRoom() {
        SavePlayerData();
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom() {
        SavePlayerData();
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    void SavePlayerData() {
        int side = int.Parse(sideSelect.text);
        int player = int.Parse(playerSelect.text);
        playerData.GetComponent<PlayerData>().UpdateData(side, player);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("MultiplayerGame");
    }
}
