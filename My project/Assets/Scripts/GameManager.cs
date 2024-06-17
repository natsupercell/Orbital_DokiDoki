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
    
    public ObjectPool pooler;
    void Awake() {
        Orb.Initialize();

        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        allyScore = enemyScore = 0;
        scoreToWin = 8;

        allyDefaultPosition = new Vector3(-4.25f,-3.75f,0f);
        enemyDefaultPosition = new Vector3(4.25f,3.75f,0f);

        pooler = GameObject.FindGameObjectsWithTag("Pooler")[0].GetComponent<ObjectPool>();
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
        pooler.cleanup();
    }

    void GameOver() {
        // TODO: Message indicating winning team should appear on screen
        Debug.Log("Game over");
        SceneManager.LoadScene("Loading");
    }
}
