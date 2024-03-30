using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorRemoverBullet : PoweredBullet {
    protected virtual void OnCollisionEnter(Collision collision) {
        PieceModel pieceModel = collision.gameObject.GetComponent<PieceModel>();
        if (pieceModel == null) {
            this.DestroyBullet();
            return;
        }

        if (this.power*1.5f >= pieceModel.parent.shieldPower)
            pieceModel.parent.shieldPower = 0;
        this.DestroyBullet();
    }
}
