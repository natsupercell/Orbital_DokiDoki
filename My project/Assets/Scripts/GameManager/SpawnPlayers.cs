using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour {

    public GameObject playerPrefab;
    private static Vector2 defaultSpawnLocation = new Vector2(0, 0);

    public void Awake() {
        //playerPrefab = Resources.Load<GameObject>("Player");
    }

    public void Start() {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, defaultSpawnLocation, Quaternion.identity);
    }
}
