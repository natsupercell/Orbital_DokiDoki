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
    public Dictionary<string, Pool> mapper;
    public Dictionary<string, Queue<GameObject>> producer;
    public Queue<GameObject> cleaner;

    public void Start() {
        mapper = new Dictionary<string, Pool>();
        producer = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools) {
            mapper.Add(pool.tag, pool);
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            producer.Add(pool.tag, queue);
        }

        cleaner = new Queue<GameObject>();
    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!producer.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        if (producer[tag].Count == 1) {
            Debug.LogWarning("Pool limit exceeded with tag " + tag);
            GameObject objectToDuplicate = producer[tag].Dequeue();
            for (int i = 0; i < 10; i++) {
                GameObject obj = Instantiate(objectToDuplicate);
                obj.SetActive(false);
                producer[tag].Enqueue(obj);
            }
        }

        GameObject objectToSpawn = producer[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        cleaner.Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void cleanUp() {
        while (cleaner.Count != 0) Destroy(cleaner.Dequeue());

        // Refilling pools
        foreach (string tag in producer.Keys){
            Pool pool = mapper[tag];
            Queue<GameObject> queue = producer[tag];
            for (int i = producer[tag].Count; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
        }
    }

    public GameObject spawnFromPool(string tag) { 
        return spawnFromPool(tag, Vector3.zero, Quaternion.identity);
    }
}
