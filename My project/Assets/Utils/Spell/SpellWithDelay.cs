using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWithDelay : Spell {
    public float delay = 0.5f;
    public SpellWithDelay(string name) : base(name) {}
    public override void activate(GameObject parent) {
        if (state == State.None) {
            aiming(parent);
            Debug.Log("aiming");
        } else {
            StartCoroutine(castSpellWithDelay(parent));
            Debug.Log("none");
        }
        switchState();
    }
    public IEnumerator castSpellWithDelay(GameObject parent) {
        crosshair.SetActive(false);
        yield return new WaitForSeconds(delay);
        castSpell(parent);
        yield return new WaitForSeconds(delay);
        parent.GetComponent<ControlAccessSwitch>().enabled = true;
    }
}
