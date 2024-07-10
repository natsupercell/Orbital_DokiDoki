using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {
    /*
        public GameObject content;
        The content is replaced by the child object
    */
    public static GameObject prefab;

    public static void Initialize() {
        prefab = Resources.Load<GameObject>("Orb/Orb");
    }

    public void Awake() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    public void Add(GameObject resource) {
        resource.transform.SetParent(transform, false);
    }

    public static void Create(GameObject resource, Transform transform) {
        if (resource != null) {
            GameObject newOrb = Instantiate(prefab, transform.position + new Vector3(0f,0f,10f), Quaternion.identity);
            newOrb.GetComponent<Orb>().Add(resource);

            Debug.Log("Created a new " + resource.GetComponent<Resource>().getName() + " orb");
        }
    }

    private GameObject GetContent() {
        return transform.GetChild(0).gameObject;
    }

    public GameObject Extract() {
        gameObject.SetActive(false);
        GameObject content = GetContent();
        content.transform.SetParent(null, false);
        content.SetActive(false);
        return content;
    }
}
