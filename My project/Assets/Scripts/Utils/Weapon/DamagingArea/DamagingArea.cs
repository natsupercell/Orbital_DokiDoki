using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DamagingArea : MonoBehaviour, AmmoType {
    public float duration = 0.05f;
    private PhotonView view;

    public void Awake() {
        view = GetComponent<PhotonView>();
    }

    public void Start() {
        StartCoroutine(DestroyAfterSeconds(duration));
    }

    private IEnumerator DestroyAfterSeconds(float duration) {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);

    }

    [PunRPC]
    public void excludeLayer(int layer) {
        gameObject.layer = Team.toLayerToBeIgnored(layer);
    }

    public void ExcludeLayerRPC(int layer) {
        view.RPC("excludeLayer", RpcTarget.All, layer);
    }
    
    void OnTriggerStay2D(Collider2D hitInfo) {
        Debug.Log("hit");
        // Implement logic for when something is within the area 
        Hitbox hitbox = hitInfo.GetComponent<Hitbox>();
        if (hitbox != null) {
            hitbox.TakeDamageRPC();
        }
    }
}
