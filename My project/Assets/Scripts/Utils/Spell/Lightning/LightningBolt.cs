using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour {
    public float duration = 0.1f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Strike());
    }

    private IEnumerator Strike() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0);
        if (hit.collider != null) {
            // Deal damage to the hit object if it has a Hitbox component
            Hitbox hitbox = hit.collider.GetComponent<Hitbox>();
            if (hitbox != null) {
                hitbox.TakeDamageRPC();
            }
        }
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
