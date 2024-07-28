using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI timerText;
    private RemainingTimer timer = null;

    public void Connect(RemainingTimer timer) {
        this.timer = timer;
    }
    void FixedUpdate() {
        if (timer != null) {
            int minutes = Mathf.FloorToInt(timer.remainingTime / 60);
            int seconds = Mathf.FloorToInt(timer.remainingTime % 60);     
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
