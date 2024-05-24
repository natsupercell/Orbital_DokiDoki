using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour {

    public GameObject playerPrefab;
    private static Vector2 defaultSpawnLocation = new Vector2(0, 0);

    private void Start() {
        PhotonNetwork.Instantiate(playerPrefab.name, defaultSpawnLocation, Quaternion.identity);
    }
}
