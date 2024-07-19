using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hitbox : MonoBehaviour {
    public bool shielded = false;
    private bool invincible;
    private bool vulnerable;
    private Team team;
    private PhotonView view;
    private float invincibleTime = 0.5f;

    public void Awake() {
        team = GetComponent<Team>();
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void TakeDamage() {
        Debug.Log("take dame");
        if (PhotonNetwork.IsMasterClient) {
            if (!invincible) {
                if (shielded && !vulnerable) {
                    shielded = false;
                    // InvincibleRPC(invincibleTime);
                    Invincible(invincibleTime);
                }
                else {
                    team?.DiedRPC();
                    gameObject.GetComponent<PhotonCustomControl>().DisableRPC();
                    // gameObject.SetActive(false);
                    Debug.Log("die");
                }
            }
        }
    }

    [PunRPC]
    private void GetShield() {
        shielded = true;
        Debug.Log("shielded");
    }

    [PunRPC]
    private void Vulnerable(float duration) {
        StartCoroutine(IVulnerable(duration));
    }

    private IEnumerator IVulnerable(float duration) {
        vulnerable = true;
        yield return new WaitForSeconds(duration);
        vulnerable = false;
    }

    [PunRPC]
    private void Invincible(float duration) {
        StartCoroutine(IInvincible(duration));
    }

    private IEnumerator IInvincible(float duration) {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    public void TakeDamageRPC() {
        view.RPC("TakeDamage", RpcTarget.All);
    }

    public void GetShieldRPC() {
        view.RPC("GetShield", RpcTarget.All);
    }

    public void VulnerableRPC(float duration) {
        view.RPC("Vulnerable", RpcTarget.All, duration);
    }

    public void InvincibleRPC(float duration) {
        view.RPC("Invincible", RpcTarget.All, duration);
    }

    /*
    public void GetTemporaryShield(float duration) {
        if (!shielded) {
            StartCoroutine(IGetTemporaryShield(duration));
        }
    }

    private IEnumerator IGetTemporaryShield(float duration) {
        shielded = true;
        yield return new WaitForSeconds(duration);
        shielded = false;
    }
*/
}
