using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour, AmmoType {
    public float laserLength = 100f;
    public float delay = 0.01f;
    public float duration = 0.1f;
    public LineRenderer lineRenderer;
    public LayerMask hitLayers; // Layers to include in raycasting

    private void Awake() {
        hitLayers = LayerMask.GetMask("Ally", "Enemy", "Terrain");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;
        StartCoroutine(ShootLaser());
    }

    public void excludeLayer(int layer) {
        string excludeLayer = LayerMask.LayerToName(layer);
        // originalLayerMask &= ~layerToRemove;
        hitLayers &= ~LayerMask.GetMask(excludeLayer);
        //Debug.Log(hitLayers.value);
    }

    private IEnumerator ShootLaser() {
        yield return new WaitForSeconds(delay);
        
        //Debug.Log(hitLayers.value);

        // Enable the line renderer and set its start position
        lineRenderer.SetPosition(0, transform.position); 

        // Calculate the end position of the laser
        Vector3 laserEndPos = transform.position + transform.right * laserLength;

        // Raycast to detect if the laser hits any objects
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserLength, hitLayers);
        if (hit.collider != null) {
            // If the laser hits an object, set the end position to the hit point
            laserEndPos = hit.point;

            // Deal damage to the hit object if it has a Hitbox component
            Hitbox hitbox = hit.collider.GetComponent<Hitbox>();
            if (hitbox != null) {
                hitbox.takeDamage();
            }
        }
        // Set the end position of the line renderer
        lineRenderer.SetPosition(1, laserEndPos);

        lineRenderer.enabled = true;

        // Wait for the duration of the laser
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
