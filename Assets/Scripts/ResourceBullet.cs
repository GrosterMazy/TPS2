using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBullet : Bullet {
    public int resourceAmount;
    protected virtual void OnCollisionEnter(Collision collision) {
        DicePiece dicePiece = collision.gameObject.GetComponent<DicePiece>();
        if (dicePiece != null) {
            dicePiece.resourceAmount =
                Mathf.Clamp(dicePiece.resourceAmount+this.resourceAmount, 0, dicePiece.maxResourceAmount);
            this.DestroyBullet();
        }
    }
}
