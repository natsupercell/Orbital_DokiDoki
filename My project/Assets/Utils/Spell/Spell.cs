using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Utility {
    public GameObject spellType;  // The prefab of the spell to cast
    public GameObject crosshair;  // A visual indicator for the target location

    public Spell(string name) : base(name) {}

    protected enum State {
        None,
        Aiming
    }

    protected State state;

    private void Awake() {
        //crosshair = Resources.Load<GameObject>("Crosshair");
    }

    private void Start() {
        state = State.None;
        crosshair = Resources.Load<GameObject>("Crosshair");
        crosshair = Instantiate(crosshair, transform.position, Quaternion.identity);
    } 

    protected void switchState() {
        if (state == State.None) state = State.Aiming;
        else state = State.None;
    }

    protected void aiming(GameObject parent) {
        crosshair.transform.position = transform.position;
        crosshair.SetActive(true);
        parent.GetComponent<ControlAccessSwitch>().enabled = false;
        parent.GetComponent<Movement>().Stop();
    }

    protected void notAiming(GameObject parent) {
        crosshair.SetActive(false);
        parent.GetComponent<ControlAccessSwitch>().enabled = true;
    }

    public override void activate(GameObject parent) {
        if (state == State.None) {
            aiming(parent);
            Debug.Log("aiming");
        } else {
            castSpell(parent);
            notAiming(parent);
            Debug.Log("none");
        }
        switchState();
    }

    public override void deactivate(GameObject parent) {
        state = State.None;
        notAiming(parent);
    }

    public virtual void castSpell(GameObject parent) {}
}
