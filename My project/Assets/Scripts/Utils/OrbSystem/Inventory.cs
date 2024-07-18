using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour {
    [System.Serializable]
    public class Slot {
        public GameObject util;
        public KeyCode key;

        public Slot(KeyCode key) {
            this.key = key;
        }

        public Utility GetUtility() {
            return util.GetComponent<Utility>();
        }

        public bool IsEmpty() {
            return util == null;
        }

        public void Activate(GameObject obj) {
            util?.GetComponent<Utility>().Activate(obj);
        }

        public void Deactivate(GameObject obj) {
            util?.GetComponent<Utility>().Deactivate(obj);
        }

        public void Enable() {
            util?.GetComponent<PhotonCustomControl>().EnableRPC();
        }

        public void Disable() {
            util?.GetComponent<PhotonCustomControl>().DisableRPC();
        }
    }

    /* NOTES:
    - The first slot is always a weapon slot, yes players now can only hold 1 weapon,
    the rest will be spells and other stuffs.
    - To be continued...
    */

    [SerializeField]
    public Slot[] slot = new Slot[2]; 
    public int energy;
    private int currentSlot;
    public KeyCode activateKey;
    public KeyCode deactivateKey;
    public KeyCode pickUpKey;
    public KeyCode dropKey;
    public PhotonView view;
    private ControlAccessSwitch control;

    private bool pickingUp;
    private bool pickUpAble = true;
    public static float delay = 0.1f;
    void Awake() {
        view = transform.parent.GetComponent<PhotonView>();
        control = transform.parent.GetComponent<ControlAccessSwitch>();
        currentSlot = 0;
        energy = 10;

        // For debugging
        slot[1].util = transform.GetChild(0).gameObject;
    }
    
    private void Update() {
        if (view.IsMine) {  
            if (control.enabled) {
                for (int i = 0; i < 2; i++) if (Input.GetKeyDown(slot[i].key)) {
                    slot[currentSlot].Disable();
                    currentSlot = i;
                    slot[currentSlot].Enable();
                    Debug.Log("Switched to slot number " + (i + 1) + ", holding "
                    + (slot[i].IsEmpty() ? "nothing" 
                    : slot[i].GetUtility().getName()));
                }
                if (Input.GetKeyDown(dropKey)) {
                    Drop();
                }
                if (Input.GetKey(pickUpKey)) {
                    pickingUp = true;
                } else {
                    pickingUp = false;
                } 
            }
            if (Input.GetKeyDown(activateKey)) {
                if (IsHoldingWeapon()) {
                    if (!slot[currentSlot].IsEmpty()) {
                        if (SpendEnergy(slot[currentSlot].GetUtility().cost)) {
                            slot[currentSlot].Activate(gameObject);
                        }
                    }
                    else Debug.Log("Weapon missing!");
                }
                else {
                    if (!slot[currentSlot].IsEmpty()) {
                        slot[currentSlot].Activate(gameObject);
                    }
                    else Debug.Log("Spell missing!");
                }
            } 
            if (Input.GetKeyDown(deactivateKey)) slot[currentSlot].Deactivate(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D box) {
        if (pickingUp && pickUpAble) {
            Orb orb = box.GetComponent<Orb>();
            if (orb == null) {
                Debug.LogWarning("Invalid object, cannot pick up");
                return;
            }
            GameObject resource = orb.Extract();
            Resource test = resource.GetComponent<Resource>();
            if (test is Weapon) StartCoroutine(PickUpWeapon(resource));
            else if (test is Shield) StartCoroutine(PickUpShield(resource));
            else if (test is Energy) StartCoroutine(RechargeEnergy(resource));
        }
    }

    private IEnumerator PickUpWeapon(GameObject weapon) {
        if (weapon != null) {
            if (!slot[0].IsEmpty()) {
                Drop();
            }
            PickUp(weapon);

            pickUpAble = false;
            yield return new WaitForSeconds(delay);
            pickUpAble = true;
        }
    }

    private void PickUp(GameObject weapon) {
        weapon.GetComponent<PhotonCustomControl>().SetParentRPC(gameObject, false);
        // weapon.transform.SetParent(gameObject.transform, false);
        slot[0].util = weapon;
        if (IsHoldingWeapon()) slot[0].Enable();
    }

    private void Drop() {
        slot[0].Enable();
        Orb.Create(slot[0].util, transform);
        slot[0].util = null;
    }

    private bool IsHoldingWeapon() {
        /* currentSlot should be 0, slot[0] is the only slot holding a weapon. */
        return currentSlot == 0;
    }

    private IEnumerator PickUpShield(GameObject shield) {
        if (shield != null) {
            transform.parent.GetComponent<Hitbox>().GetShieldRPC();

            pickUpAble = false;
            yield return new WaitForSeconds(delay);
            pickUpAble = true;
        }
    }

    private IEnumerator RechargeEnergy(GameObject energyObj) {
        Energy energy = energyObj.GetComponent<Energy>();
        if (energy != null) {
            this.energy += energy.getValue();
            Debug.Log("Current energy value: " + this.energy);

            pickUpAble = false;
            yield return new WaitForSeconds(delay);
            pickUpAble = true;
        }
    }

    private bool SpendEnergy(int cost) {
        if (energy < cost) {
            Debug.Log("Out of energy!");
            return false;
        }
        energy -= cost; return true;
    }
}
