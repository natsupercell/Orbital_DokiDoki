using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : TargetedSpell {
    private GameObject bolt;
    public Lightning() : base("lightning") {}

    private void Awake() {
        string prefabPath = "LightningBolt";
        bolt = Resources.Load<GameObject>(prefabPath);
    }

    public override void castSpell(GameObject parent) {
        Instantiate(bolt, crosshair.transform.position, Quaternion.identity);
    }
}
