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
    public bool roundStarted;

    public GameObject allyPrefab;
    public GameObject enemyPrefab;
    
    //public ObjectPool pooler;
    void Awake() {
        Ally.reset();
        Enemy.reset();

        allyPrefab = Resources.Load<GameObject>("Player");
        enemyPrefab = Resources.Load<GameObject>("PlayerClone");

        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);

        Orb.Initialize();

        allyScore = enemyScore = 0;
        scoreToWin = 3;

        allyDefaultPosition = new Vector3(-4.25f,-3.75f,10f);
        enemyDefaultPosition = new Vector3(4.25f,3.75f,10f);

        Instantiate(allyPrefab, allyDefaultPosition, Quaternion.identity);
        Instantiate(enemyPrefab, enemyDefaultPosition, Quaternion.identity);

        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        roundStarted = false;
        StartCoroutine(startNewRound());

        //pooler = GameObject.FindGameObjectsWithTag("Pooler")[0].GetComponent<ObjectPool>();
    }

    void Update() {
        if (roundStarted) {
            if (Ally.isEliminated()) {
                enemyScore++; Debug.Log("Enemy team win");
                Debug.Log("Current score: " + allyScore + " " + enemyScore);
                if (enemyScore >= scoreToWin) GameOver();
                StartCoroutine(startNewRound());
            }
            if (Enemy.isEliminated()) {
                allyScore++; Debug.Log("Ally team win");
                Debug.Log("Current score: " + allyScore + " " + enemyScore);
                if (allyScore >= scoreToWin) GameOver();
                StartCoroutine(startNewRound());
            }
        }
    }

    private IEnumerator startNewRound() {
        roundStarted = false;

        cleanup();

        foreach (GameObject p in allies) {
            //Debug.Log("an ally");
            p.GetComponent<Resetter>().reset();
            p.transform.position = allyDefaultPosition;
        }
        foreach (GameObject p in enemies) {
            //Debug.Log("an enemy");
            p.GetComponent<Resetter>().reset();
            p.transform.position = enemyDefaultPosition;
        }

        freezeAll();
        yield return new WaitForSeconds(3f);
        unfreezeAll();

        roundStarted = true;
        //pooler.cleanup();
    }

    private void freezeAll() {
        foreach (GameObject p in allies) p.GetComponent<ControlAccessSwitch>().disable();
        foreach (GameObject p in enemies) p.GetComponent<ControlAccessSwitch>().disable();
        //Debug.Log("frozen");
    }

    private void unfreezeAll() {
        foreach (GameObject p in allies) p.GetComponent<ControlAccessSwitch>().enabled = true;
        foreach (GameObject p in enemies) p.GetComponent<ControlAccessSwitch>().enabled = true;
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
