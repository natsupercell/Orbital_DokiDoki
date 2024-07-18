using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Utility {
    public GameObject spellType;  // The prefab of the spell to cast
    public GameObject crosshair;  // A visual indicator for the target location
    public float delay = 0.05f;
    public Spell(string name) : base(name) {}

    protected enum State {
        None,
        Aiming
    }

    protected State state;

    public virtual void Awake() {
        state = State.None;
        crosshair = transform.GetChild(0).gameObject;
    }

    private void Start() {} 

    protected void switchState() {
        if (state == State.None) state = State.Aiming;
        else state = State.None;
    }

    protected void aiming(GameObject parent) {
        GameObject realParent = parent.transform.parent.gameObject;
        crosshair.transform.position = realParent.transform.position;
        crosshair.SetActive(true);
        realParent.GetComponent<ControlAccessSwitch>().DisableRPC();
    }

    protected void notAiming(GameObject parent) {
        GameObject realParent = parent.transform.parent.gameObject;
        crosshair.SetActive(false);
        realParent.GetComponent<ControlAccessSwitch>().EnableRPC();
    }

    public override void Activate(GameObject parent) {
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
        GameObject realParent = parent.transform.parent.gameObject;
        crosshair.SetActive(false);
        yield return new WaitForSeconds(delay);
        castSpell(parent);
        yield return new WaitForSeconds(delay);
        realParent.GetComponent<ControlAccessSwitch>().EnableRPC();
    }

    public override void Deactivate(GameObject parent) {
        state = State.None;
        notAiming(parent);
    }

    public virtual void castSpell(GameObject parent) {}
}
