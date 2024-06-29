using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Team {
    public static int toLayerToBeIgnored(int layer) {
        return layer + 2;
    }
    void alive();
    void died();
    int toLayer();
}
