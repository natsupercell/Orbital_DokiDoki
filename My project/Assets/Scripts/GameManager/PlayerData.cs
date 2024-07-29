using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public int side;
    public int player;

    public void UpdateData(int side, int player) {
        if (side > 2 || side < 1 || player > 2 || player < 1) {
            throw new System.ArgumentOutOfRangeException("Parameter index is out of range.");
        }
        this.side = side;
        this.player = player;
    }
}
