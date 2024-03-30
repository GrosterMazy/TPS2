using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredPieceShoot : PieceShoot {
    protected override void Update() {
        if (Input.GetMouseButtonDown(0)
                && this.piece.resourceAmount > 0
                && this.piece.actionsRemain > 0) {
            spawnPoint.LookAt(aim.position);
            GameObject projectile = Instantiate(
                this.projectilePrefab,
                this.spawnPoint.position+this.spawnPoint.forward,
                this.spawnPoint.rotation
            );
            this.piece.pieceManager.PlaySound(this.piece.shootSound);
            PoweredBullet poweredBullet = projectile.GetComponent<PoweredBullet>();
            poweredBullet.power = this.piece.power;
            

            this.piece.resourceAmount--;
            this.piece.movesRemain = 0;
            this.piece.actionsRemain--;
        }
    }
}
