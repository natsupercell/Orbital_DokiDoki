using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks {
    // Start is called before the first frame update
    private void Start() {
        PhotonNetwork.NetworkingClient.AppId = "4ad07916-a8bb-4790-a79a-8930a20bee76";
        PhotonNetwork.ConnectUsingSettings();
        // PhotonNetwork.ConnectToRegion("asia");
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        SceneManager.LoadScene("Lobby");
    }
}
