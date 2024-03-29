using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBullet : Bullet {
    public int resourceAmount;
    protected virtual void OnCollisionEnter(Collision collision) {
        PieceModel pieceModel = collision.gameObject.GetComponent<PieceModel>();
        if (pieceModel == null) {
            this.DestroyBullet();
            return;
        }

        DicePiece dicePiece = collision.gameObject.GetComponent<PieceModel>().parent;
        if (dicePiece == null) {
            this.DestroyBullet();
            return;
        }
        
        dicePiece.resourceAmount =
            Mathf.Clamp(dicePiece.resourceAmount+this.resourceAmount, 0, dicePiece.maxResourceAmount);
        this.DestroyBullet();
    }
}
