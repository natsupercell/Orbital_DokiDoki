using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour {
    private static Vector2 defaultSpawnLocation = new Vector3(0, 0, 0);
    private ObjectPool objectPool;

    public void Awake() {
        //playerPrefab = Resources.Load<GameObject>("Player");
    }

    public void Start() {
        if (PhotonNetwork.IsMasterClient) {
            GameObject orbSpawner = PhotonNetwork.Instantiate("zEverythingElse/OrbSpawner", new Vector3(0, -1, 30), Quaternion.identity);
            objectPool = orbSpawner.GetComponent<ObjectPool>();
            PhotonNetwork.Instantiate("Players/Player", defaultSpawnLocation, Quaternion.identity);
            transform.parent.GetComponent<GameManager>().orbSpawner = orbSpawner.GetComponent<OrbSpawner>();
        }
        else {
            PhotonNetwork.Instantiate("Players/PlayerClone", defaultSpawnLocation, Quaternion.identity);
            Destroy(gameObject);
        }
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
