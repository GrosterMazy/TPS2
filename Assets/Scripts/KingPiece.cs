using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : DicePiece {
    protected override void OnDestroy() {
        base.OnDestroy();
        this.pieceManager.teams[this.team].teamKings.Remove(this);
    }
}
