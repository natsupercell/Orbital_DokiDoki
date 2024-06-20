using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour {
    [System.Serializable]
    public class Slot {
        public Utility util;
        public KeyCode key;

        public bool isEmpty() {
            return (util == null);
        }

        public void activate(GameObject obj) {
            if (!isEmpty()) {
                util.activate(obj);
                if (util.destroy()) {
                    Destroy(util);
                }
            }
        }

        public void deactivate(GameObject obj) {
            if (!isEmpty()) {
                util.deactivate(obj);
            }
        }

        public virtual void pickUp(Weapon weapon, GameObject obj) {}
        public virtual void drop(GameObject obj) {}
    }
    public class WeaponSlot : Slot {
        public override void pickUp(Weapon weapon, GameObject obj) {
            if (!isEmpty()) {
                drop(obj);
            }
            this.util = weapon;
            Debug.Log("Picked up " + weapon.name);
        }

        public override void drop(GameObject obj) {
            if (!isEmpty()) {
                Orb.create(util, obj.transform);
                Debug.Log("Dropped " + util.name);
                util = null;
            } else Debug.Log("Can't drop, slot is empty");
        }
    }

    public class SpellSlot : Slot {}

    [SerializeField]
    public Slot[] slot; 
    public int energy;
    private int currentSlot;
    public KeyCode activateKey;
    public KeyCode deactivateKey;
    public KeyCode pickUpKey;
    public KeyCode dropKey;
    public PhotonView view;
    private ControlAccessSwitch control;

    private bool pickingUp;

    void Awake() {
        view = GetComponent<PhotonView>();
        control = GetComponent<ControlAccessSwitch>();
        currentSlot = 0;
        energy = 10;
        /*
        slot[0] = new WeaponSlot();
        slot[0].key = KeyCode.Alpha1;
        slot[1] = new WeaponSlot();
        slot[1].key = KeyCode.Alpha2;
        slot[2] = new SpellSlot();
        slot[2].key = KeyCode.Alpha3;
        */
    }
    
    private void Update() {
        if (view.IsMine) {  
            if (control.enabled) {
                for (int i = 0; i < 3; i++) if (Input.GetKeyDown(slot[i].key)) {
                    currentSlot = i;
                    Debug.Log("Switched to slot number " + (i + 1) + ", holding " 
                    + (slot[i].isEmpty() ? "nothing" : slot[i].util.name));
                }
                if (Input.GetKeyDown(dropKey)) {
                    slot[currentSlot].drop(gameObject);
                }
                if (Input.GetKey(pickUpKey)) {
                    pickingUp = true;
                } else {
                    pickingUp = false;
                } 
            }
            if (Input.GetKeyDown(activateKey)) {
                if (!slot[currentSlot].isEmpty())
                    if(!isWeapon() || spendEnergy(slot[currentSlot].util.cost)) {
                        slot[currentSlot].activate(gameObject);
                }
                else Debug.Log("Weapon missing!");
            } 
            if (Input.GetKeyDown(deactivateKey)) slot[currentSlot].deactivate(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D box) {
        if (pickingUp) {
            Orb orb = box.GetComponent<Orb>();
            if (orb == null) {
                Debug.LogWarning("Invalid object, cannot pick up");
                return;
            }
            Resource resource = orb.extract();
            if (resource is Weapon) pickUpWeapon((Weapon) resource);
            else if (resource is Energy) rechargeEnergy((Energy) resource);

        }
    }

    private void pickUpWeapon(Weapon weapon) {
        if (weapon != null) {
            if (isWeapon()) {
                if (slot[currentSlot].isEmpty()) {
                    slot[currentSlot].pickUp(weapon, gameObject);
                } else if (slot[0].isEmpty()) {
                    slot[0].pickUp(weapon, gameObject);
                } else if (slot[1].isEmpty()) {
                    slot[1].pickUp(weapon, gameObject);
                } else {
                    slot[currentSlot].pickUp(weapon, gameObject);
                } 
            }
            else slot[0].pickUp(weapon, gameObject);
        }
    }

    private bool isWeapon() {
        return currentSlot < 2;
    }

    private void rechargeEnergy(Energy energy) {
        if (energy != null) {
            this.energy += energy.getValue();
        }
    }

    private bool spendEnergy(int cost) {
        if (energy < cost) {
            Debug.Log("Out of energy!");
            return false;
        }
        energy -= cost; return true;
    }
}
