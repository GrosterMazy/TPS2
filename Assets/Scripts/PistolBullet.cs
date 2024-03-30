using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : PoweredBullet {
    protected virtual void OnCollisionEnter(Collision collision) {
        PieceModel pieceModel = collision.gameObject.GetComponent<PieceModel>();
        if (pieceModel == null) {
            this.DestroyBullet();
            return;
        }

        if (this.power > pieceModel.parent.shieldPower + 2 || pieceModel.parent.shieldPower == 0)
            Destroy(pieceModel.parent.gameObject);
        this.DestroyBullet();
    }
}
