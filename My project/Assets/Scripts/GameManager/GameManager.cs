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
    private CustomTilemap map;

/*
    public GameObject allyPrefab;
    public GameObject enemyPrefab;
*/

    public void Awake() {
        map = GameObject.FindGameObjectsWithTag("Map")[0].GetComponent<CustomTilemap>();
        if (map != null) Debug.Log("map found!");
    }

    
    public void InitializeGame() {
        Ally.Reset();
        Enemy.Reset();

        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);

        allyScore = enemyScore = 0;
        scoreToWin = 3;

        allyDefaultPosition = new Vector3(-4.25f,-3.75f,10f);
        enemyDefaultPosition = new Vector3(4.25f,3.75f,10f);

        // PhotonNetwork.Instantiate(allyPrefab, allyDefaultPosition, Quaternion.identity);
        // PhotonNetwork.Instantiate(enemyPrefab, enemyDefaultPosition, Quaternion.identity);

        allies.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        roundStarted = false;
        StartCoroutine(startNewRound());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (PhotonNetwork.IsMasterClient) {
                InitializeGame();
                Debug.Log("Game Starts. GL, HF!");
            }
        }

        if (roundStarted) {
            if (Ally.IsEliminated()) {
                enemyScore++; Debug.Log("Enemy team win");
                Debug.Log("Current score: " + allyScore + " " + enemyScore);
                roundStarted = false;
                if (enemyScore >= scoreToWin) StartCoroutine(GameOver());
                else {
                    StartCoroutine(CleanupThenStartNewRound());
                }
            }
            if (Enemy.IsEliminated()) {
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

    private IEnumerator startNewRound() {
        map.Reset();

        foreach (GameObject p in allies) {
            //Debug.Log("an ally");
            p.GetComponent<Resetter>().reset();
            p.GetComponent<PhotonCustomControl>().MoveRPC(allyDefaultPosition);
        }
        foreach (GameObject p in enemies) {
            //Debug.Log("an enemy");
            p.GetComponent<Resetter>().reset();
            p.GetComponent<PhotonCustomControl>().MoveRPC(enemyDefaultPosition);
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
        foreach (GameObject p in allies) p.GetComponent<ControlAccessSwitch>().DisableRPC();
        foreach (GameObject p in enemies) p.GetComponent<ControlAccessSwitch>().DisableRPC();
        //Debug.Log("frozen");
    }

    private void unfreezeAll() {
        foreach (GameObject p in allies) p.GetComponent<ControlAccessSwitch>().EnableRPC();
        foreach (GameObject p in enemies) p.GetComponent<ControlAccessSwitch>().EnableRPC(); 
    }

    private IEnumerator CleanupThenStartNewRound() {
        yield return new WaitForSeconds(3f);
        
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orb")) {
            p.GetComponent<PhotonCustomControl>().DisableRPC();
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
