using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour {
    
    public MouseSelection mouseSelection;
    public GameObject shootButton;
    public GameObject mineButton;
    public GameObject boardCamera;
    public PieceMovement pieceMovement;
    public PieceManager pieceManager;
    public BoardGeneration board;

    void Update() {
        if (this.mouseSelection.selected != null) {
            PieceModel pieceModel = this.mouseSelection.selected.GetComponent<PieceModel>();
            if (pieceModel != null && pieceManager.TurnOf(pieceModel.parent)) {
                // Shoot button
                if (pieceModel.parent.resourceAmount > 0
                        && pieceModel.parent.gameObject.GetComponent<ShootingPiece>() != null
                        && pieceModel.parent.actionsRemain > 0) {
                    this.shootButton.SetActive(true);
                } else this.shootButton.SetActive(false);
                // Mine button
                if (pieceModel.parent.gameObject.GetComponent<MinerPiece>()
                        && this.board.cells[pieceModel.parent.boardX][pieceModel.parent.boardZ]
                            .GetComponent<WithResource>() != null
                        && pieceModel.parent.resourceAmount < pieceModel.parent.maxResourceAmount) {
                    this.mineButton.SetActive(true);
                } else this.mineButton.SetActive(false);

                return;
            }
        }
        this.shootButton.SetActive(false);
        this.mineButton.SetActive(false);
    }

    public void ShootButton() {
        this.boardCamera.SetActive(false);
        GameObject pieceCamera = this.mouseSelection.selected.GetComponent<PieceModel>()
            .parent.gameObject.GetComponent<ShootingPiece>().localCameraLink;
        pieceCamera.SetActive(true);
        pieceCamera.GetComponent<PieceCamera>().Init();
        this.pieceMovement.UndoColoring();
        this.mouseSelection.UndoColoring();
    }

    public void MineButton() {
        DicePiece parent = this.mouseSelection.selected.GetComponent<PieceModel>().parent;
        parent.resourceAmount += this.board.cells[parent.boardX][parent.boardZ]
            .GetComponent<WithResource>().TakeResource(1);
        parent.movesRemain = 0;
        parent.actionsRemain--;
    }
}
