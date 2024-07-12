using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks {
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

    public void Awake() {
        // if (PhotonNetwork.IsMasterClient) {
            InitializeGame();
        // }
    }
    
    public void InitializeGame() {
        Ally.reset();
        Enemy.reset();

        allyPrefab = Resources.Load<GameObject>("Players/Player");
        enemyPrefab = Resources.Load<GameObject>("Players/PlayerClone");

        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);

        Orb.Initialize();

        allyScore = enemyScore = 0;
        scoreToWin = 3;

        allyDefaultPosition = new Vector3(-4.25f,-3.75f,10f);
        enemyDefaultPosition = new Vector3(4.25f,3.75f,10f);

        // PhotonNetwork.Instantiate(allyPrefab, allyDefaultPosition, Quaternion.identity);
        // PhotonNetwork.Instantiate(enemyPrefab, enemyDefaultPosition, Quaternion.identity);

        // allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        // enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        roundStarted = false;
        // StartCoroutine(startNewRound());
    }

/*
    void Update() {
        if (roundStarted) {
            if (Ally.isEliminated()) {
                enemyScore++; Debug.Log("Enemy team win");
                Debug.Log("Current score: " + allyScore + " " + enemyScore);
                roundStarted = false;
                if (enemyScore >= scoreToWin) StartCoroutine(GameOver());
                else {
                    StartCoroutine(CleanupThenStartNewRound());
                }
            }
            if (Enemy.isEliminated()) {
                allyScore++; Debug.Log("Ally team win");
                Debug.Log("Current score: " + allyScore + " " + enemyScore);
                roundStarted = false;
                if (allyScore >= scoreToWin) StartCoroutine(GameOver());
                else {
                    StartCoroutine(CleanupThenStartNewRound());
                }
            }
        }
    }
*/

    private IEnumerator startNewRound() {
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
        Debug.Log(3);
        yield return new WaitForSeconds(1f);
        Debug.Log(2);
        yield return new WaitForSeconds(1f);
        Debug.Log(1);
        yield return new WaitForSeconds(1f);
        Debug.Log("GO!");
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

    private IEnumerator CleanupThenStartNewRound() {
        yield return new WaitForSeconds(3f);
        
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orb")) {
            Destroy(p);
        }

        StartCoroutine(startNewRound());
    }

    private IEnumerator GameOver() {
        // TODO: Message indicating winning team should appear on screen
        Debug.Log("Game over");
        yield return new WaitForSeconds(1f);
        Debug.Log("Redirecting to the main menu...");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
