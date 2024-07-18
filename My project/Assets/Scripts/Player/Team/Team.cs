using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Team {
    public static int toLayerToBeIgnored(int layer) {
        return layer + 2;
    }
    void AliveRPC();
    void DiedRPC();
    int toLayer();
}
