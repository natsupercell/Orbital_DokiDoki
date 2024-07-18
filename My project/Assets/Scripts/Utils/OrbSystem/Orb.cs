using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Orb : MonoBehaviour {
    /*
        public GameObject content;
        The content is replaced by the child object
    */
    public static GameObject prefab;
    public static string prefabPath;
    
    public void Awake() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        prefabPath = "PoolingPrefabs/Orb";
        prefab = Resources.Load<GameObject>(prefabPath);
    }

    public void Add(GameObject resource) {
        resource.GetComponent<PhotonCustomControl>().SetParentRPC(gameObject, false);
    }

    public static void Create(GameObject resource, Transform transform) {
        if (resource != null) {
            GameObject newOrb = PhotonNetwork.Instantiate(prefabPath, transform.position + new Vector3(0f,0f,10f), Quaternion.identity);
            newOrb.GetComponent<Orb>().Add(resource);

            Debug.Log("Created a new " + resource.GetComponent<Resource>().getName() + " orb");
        }
    }

    private GameObject GetContent() {
        return transform.GetChild(0).gameObject;
    }

    public GameObject Extract() {
        gameObject.GetComponent<PhotonCustomControl>().DisableRPC();
        GameObject content = GetContent();
        content.GetComponent<PhotonCustomControl>().SetParentRPC(null, false);
        // content.transform.SetParent(null, false);
        content.GetComponent<PhotonCustomControl>().DisableRPC();
        return content;
    }
}
