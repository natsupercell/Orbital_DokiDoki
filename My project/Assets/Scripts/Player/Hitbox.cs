using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public bool shielded;
    private bool invincible;
    private bool vulnerable;
    private Team team;

    public void Awake() {
        team = GetComponent<Team>();
    }

    public void takeDamage() {
        if (!invincible) {
            if (shielded && !vulnerable) {
                shielded = false;
                Invincible(0.5f);
            }
            else {
                team.died();
                gameObject.GetComponent<PhotonCustomControl>().DisableRPC();
            }
        }
    }

    public void GetShield() {
        shielded = true;
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

    public void Vulnerable(float duration) {
        StartCoroutine(IVulnerable(duration));
    }

    private IEnumerator IVulnerable(float duration) {
        vulnerable = true;
        yield return new WaitForSeconds(duration);
        vulnerable = false;
    }

    public void Invincible(float duration) {
        StartCoroutine(IInvincible(duration));
    }

    private IEnumerator IInvincible(float duration) {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
}
