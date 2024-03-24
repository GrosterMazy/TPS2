using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class TeamInfo {
    public Material material;
    public string turnText;

    public TeamInfo(Material material, string turnText) {
        this.material = material;
        this.turnText = turnText;
    }
}
