using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingArea : MonoBehaviour, AmmoType {
    public float duration = 0.05f;
    public void Start() {
        StartCoroutine(DestroyAfterSeconds(duration));
    }
    private IEnumerator DestroyAfterSeconds(float duration) {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    public void excludeLayer(int layer) {
        gameObject.layer = Team.toLayerToBeIgnored(layer);
    }
    void OnTriggerStay2D(Collider2D hitInfo) {
        // Implement logic for when something is within the area 
        Hitbox hitbox = hitInfo.GetComponent<Hitbox>();
        if (hitbox != null) {
            hitbox.takeDamage();
        }
    }
}
