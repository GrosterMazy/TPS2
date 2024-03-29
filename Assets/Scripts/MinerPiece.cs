using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerPiece : ShootingPiece {
    protected override void Update() {
        base.Update();
        // has 2 actions on high power
        if ((int)(this.power / 4) == 1) // 4, 5 or 6
            this.actions = 2;
        else this.actions = 1;
    }
}
