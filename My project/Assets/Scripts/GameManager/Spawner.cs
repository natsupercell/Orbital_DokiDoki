using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour {
    private static Vector2 defaultSpawnLocation = new Vector3(0, 0, 0);
    private ObjectPool objectPool;

    public void Awake() {
        
    }

    public void Start() {
        PlayerData playerData = GameObject.FindGameObjectsWithTag("Data")[0].GetComponent<PlayerData>();
        if (playerData.side == 1) {
            if (playerData.player == 1) {
                PhotonNetwork.Instantiate("Players/Player", defaultSpawnLocation, Quaternion.identity);
            } 
            else if (playerData.player == 2) {
                PhotonNetwork.Instantiate("Players/PlayerCloneOppo", defaultSpawnLocation, Quaternion.identity);
            }
        }
        else if (playerData.side == 2) {
            if (playerData.player == 1) {
                PhotonNetwork.Instantiate("Players/PlayerOppo", defaultSpawnLocation, Quaternion.identity);
            } 
            else if (playerData.player == 2) {
                PhotonNetwork.Instantiate("Players/PlayerClone", defaultSpawnLocation, Quaternion.identity);
            }
        }
        if (PhotonNetwork.IsMasterClient) {
            GameObject orbSpawner = PhotonNetwork.Instantiate("zEverythingElse/OrbSpawner", new Vector3(0, -1, 30), Quaternion.identity);
            objectPool = orbSpawner.GetComponent<ObjectPool>();
            transform.parent.GetComponent<GameManager>().orbSpawner = orbSpawner.GetComponent<OrbSpawner>();
        }
        else {
            Destroy(gameObject);
        }
        Destroy(playerData.gameObject);
    }
    
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (PhotonNetwork.IsMasterClient) {
                objectPool.LoadPrefabs();
                Destroy(gameObject);
            }
        }
    }
}
