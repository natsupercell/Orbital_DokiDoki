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
                gameObject.SetActive(false);
            }
        }
    }

    public void Vulnerable(float duration) {
        StartCoroutine(IVulnerable(duration));
    }

    public void Invincible(float duration) {
        StartCoroutine(IInvincible(duration));
    }

    private IEnumerator IVulnerable(float duration) {
        vulnerable = true;
        yield return new WaitForSeconds(duration);
        vulnerable = false;
    }

    private IEnumerator IInvincible(float duration) {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
}
