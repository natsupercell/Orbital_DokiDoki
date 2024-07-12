using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour {
    private static Vector2 defaultSpawnLocation = new Vector3(0, 0, 0);

    public void Awake() {
        //playerPrefab = Resources.Load<GameObject>("Player");
    }

    public void Start() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate("Players/Player", defaultSpawnLocation, Quaternion.identity);
        }
        else {
            PhotonNetwork.Instantiate("Players/PlayerClone", defaultSpawnLocation, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (PhotonNetwork.IsMasterClient) {
                PhotonNetwork.Instantiate("zEverythingElse/OrbSpawner", new Vector3(0, -1, 30), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
