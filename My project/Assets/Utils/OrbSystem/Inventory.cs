using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour {
    [System.Serializable]
    public class WeaponSlot {
        public Weapon weapon;
        public KeyCode key;

        public bool isEmpty() {
            return (weapon == null);
        }

        public void pickUp(Weapon weapon, GameObject obj) {
            if (!isEmpty()) {
                drop(obj);
            }
            this.weapon = weapon;
            Debug.Log("Picked up " + weapon.name);
        }

        public void drop(GameObject obj) {
            if (!isEmpty()) {
                Orb.create(weapon, obj.transform);
                Debug.Log("Dropped " + weapon.name);
                weapon = null;
            } else Debug.Log("Can't drop, slot is empty");
        }

        public void activate(GameObject obj) {
            if (!isEmpty()) {
                weapon.activate(obj);
                if (weapon.destroy()) {
                    Destroy(weapon);
                    // weapon = null;
                }
            }
        }
        
        public void deactivate(GameObject obj) {
            if (!isEmpty()) {
                weapon.deactivate(obj);
            }
        }
    }

    [SerializeField]
    public WeaponSlot[] slot = new WeaponSlot[2];
    public int energy;
    private int currentSlot;
    public KeyCode activateKey;
    public KeyCode deactivateKey;
    public KeyCode pickUpKey;
    public KeyCode dropKey;
    public PhotonView view;
    private ControlAccessSwitch control;

    private bool pickingUp;

    private void Start() {
        view = GetComponent<PhotonView>();
        control = GetComponent<ControlAccessSwitch>();
        currentSlot = 0;
        energy = 10;
    }
    
    private void Update() {
        if (view.IsMine) {  
            if (control.enabled) {
                for (int i = 0; i < 3; i++) if (Input.GetKeyDown(slot[i].key)) {
                    currentSlot = i;
                    Debug.Log("Switched to slot number " + (i + 1) + ", holding " 
                    + (slot[i].isEmpty() ? "nothing" : slot[i].weapon.name));
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
                if (!slot[currentSlot].isEmpty() && spendEnergy(slot[currentSlot].weapon.cost)) slot[currentSlot].activate(gameObject);
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
