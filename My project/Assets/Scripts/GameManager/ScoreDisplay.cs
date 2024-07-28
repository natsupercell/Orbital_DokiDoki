using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI allyScoreDisplay;
    [SerializeField] TextMeshProUGUI enemyScoreDisplay;
    private PhotonView view;

    void Awake() {
        view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void UpdateScore(int allyScore, int enemyScore) {
        allyScoreDisplay.text = allyScore.ToString();
        enemyScoreDisplay.text = enemyScore.ToString();
    }

    public void UpdateScoreRPC(int allyScore, int enemyScore) {
        view.RPC("UpdateScore", RpcTarget.All, allyScore, enemyScore);
    }
}
