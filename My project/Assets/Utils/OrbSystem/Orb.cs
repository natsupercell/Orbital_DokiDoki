using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
    public Resource content;
    public static GameObject prefab;

    public static void Initialize() {
        prefab = Resources.Load<GameObject>("Orb/Orb");
    }

    public void Awake() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    public static void create(Resource resource, Transform transform) {
        if (resource != null) {
            GameObject newOrb = Instantiate(prefab, transform.position + new Vector3(0f,0f,-10f), Quaternion.identity);
            newOrb.GetComponent<Orb>().content = resource;

            Debug.Log("Created a new " + resource + " orb");
        }
    }

    public Resource extract() {
        gameObject.SetActive(false);
        Resource output = content;
        content = null;
        return output;
    }
}
