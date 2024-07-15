using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;

public class ObjectPool : MonoBehaviourPunCallbacks {
    public string GetResourcesPath(string tag) {
        return "PoolingPrefabs/" + tag;
    } 

    [System.Serializable]
    public class Pool {
        public string tag;
        public int size;
        Pool(string tag, int size) {
            this.tag = tag;
            this.size = size;
        }
    }
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> producer;
    public PhotonView view;

    public void Awake() {
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient) {
            LoadPrefabs();
        }
    }
    

    public void LoadPrefabs() {
        producer = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools) {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = PhotonNetwork.Instantiate(GetResourcesPath(pool.tag), new Vector3(0,0,0), Quaternion.identity);
                obj.GetComponent<PhotonCustomControl>().DisableRPC();
                queue.Enqueue(obj);
            }

            producer.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!producer.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = producer[tag].Dequeue(); 

        objectToSpawn.GetComponent<PhotonCustomControl>().EnableRPC();
        objectToSpawn.GetComponent<PhotonCustomControl>().MoveRPC(position, rotation);

        producer[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnFromPool(string tag) { 
        return SpawnFromPool(tag, Vector3.zero, Quaternion.identity);
    }

    /*
    public void cleanup() {
        while (cleaner.Count != 0) {
            GameObject obj = cleaner.Dequeue();
            obj.SetActive(false);
        }
    }
    */
}
