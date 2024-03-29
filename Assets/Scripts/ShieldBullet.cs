using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : PoweredBullet {
    protected virtual void OnCollisionEnter(Collision collision) {
        PieceModel pieceModel = collision.gameObject.GetComponent<PieceModel>();
        if (pieceModel == null) {
            this.DestroyBullet();
            return;
        }

        pieceModel.parent.shieldPower = this.power;
        this.DestroyBullet();
    }
}
