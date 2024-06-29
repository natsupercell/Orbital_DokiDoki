using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public List<GameObject> allies;
    public List<GameObject> enemies;
    public int allyScore;
    public int enemyScore;
    public int scoreToWin;
    public Vector3 allyDefaultPosition;
    public Vector3 enemyDefaultPosition;
    
    //public ObjectPool pooler;
    void Awake() {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);

        Orb.Initialize();

        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        allyScore = enemyScore = 0;
        scoreToWin = 3;

        allyDefaultPosition = new Vector3(-4.25f,-3.75f,10f);
        enemyDefaultPosition = new Vector3(4.25f,3.75f,10f);

        //pooler = GameObject.FindGameObjectsWithTag("Pooler")[0].GetComponent<ObjectPool>();
    }

    void Update() {
        if (Ally.isEliminated()) {
            enemyScore++; Debug.Log("Enemy team win");
            if (enemyScore >= scoreToWin) GameOver();
            startNewRound();
        }
        if (Enemy.isEliminated()) {
            allyScore++; Debug.Log("Ally team win");
            if (allyScore >= scoreToWin) GameOver();
            startNewRound();
        }
    }

    void startNewRound() {
        foreach (GameObject p in allies) {
            p.GetComponent<Resetter>().reset();
            p.transform.position = allyDefaultPosition;
        }
        foreach (GameObject p in enemies) {
            p.GetComponent<Resetter>().reset();
            p.transform.position = enemyDefaultPosition;
        }
        cleanup();
        //pooler.cleanup();
    }

    void cleanup() {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orb")) {
            Destroy(p);
        }
    }

    void GameOver() {
        // TODO: Message indicating winning team should appear on screen
        Debug.Log("Game over");
        SceneManager.LoadScene("MainMenu");
    }
}
