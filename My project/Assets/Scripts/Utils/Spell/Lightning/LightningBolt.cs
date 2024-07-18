using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightningBolt : MonoBehaviour {
    public float duration = 1f;
    private GameObject damagingArea;
    private string ammoPath;

    void Awake() {
        ammoPath = "AmmoTypes/RocketExplosiveArea";
        damagingArea = Resources.Load<GameObject>(ammoPath);
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Strike());
    }

    private IEnumerator Strike() {
        yield return new WaitForSeconds(duration);
        PhotonNetwork.Instantiate(ammoPath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
