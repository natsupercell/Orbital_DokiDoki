using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
        Pool(string tag, GameObject prefab, int size) {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> producer;
    public Queue<GameObject> cleaner;

    public void Start() {
        producer = new Dictionary<string, Queue<GameObject>>();
        cleaner = new Queue<GameObject>();
        foreach (Pool pool in pools) {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            producer.Add(pool.tag, queue);
        }
    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!producer.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = producer[tag].Dequeue(); 

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        producer[tag].Enqueue(objectToSpawn);
        cleaner.Enqueue(objectToSpawn);

        // Debug.Log(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject spawnFromPool(string tag) { 
        return spawnFromPool(tag, Vector3.zero, Quaternion.identity);
    }

    public void cleanup() {
        while (cleaner.Count != 0) {
            GameObject obj = cleaner.Dequeue();
            obj.SetActive(false);
        }
    }
}
