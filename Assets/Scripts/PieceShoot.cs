using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShoot : MonoBehaviour {
    public GameObject projectilePrefab;
    public DicePiece piece;
    public Transform spawnPoint;
    public Transform aim;

    void Update() {
        if (Input.GetMouseButtonDown(0)
                && this.piece.resourceAmount > 0
                && this.piece.actionsRemain > 0) {
            spawnPoint.LookAt(aim.position);
            Instantiate(this.projectilePrefab, this.spawnPoint.position+this.spawnPoint.forward, this.spawnPoint.rotation);
            this.piece.resourceAmount--;
            this.piece.movesRemain = 0;
            this.piece.actionsRemain--;
        }
    }
}
