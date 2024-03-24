using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour {
    
    public MouseSelection mouseSelection;
    public GameObject shootButton;
    public GameObject boardCamera;
    public PieceMovement pieceMovement;
    public PieceManager pieceManager;

    void Update() {
        if (this.mouseSelection.selected != null) {
            PieceModel pieceModel = this.mouseSelection.selected.GetComponent<PieceModel>();
            if (pieceModel != null && pieceManager.TurnOf(pieceModel.parent))
                this.shootButton.SetActive(true);
            else this.shootButton.SetActive(false);
        }
        else this.shootButton.SetActive(false);
    }

    public void ShootButton() {
        this.boardCamera.SetActive(false);
        GameObject pieceCamera = this.mouseSelection.selected.GetComponent<PieceModel>().parent.localCameraLink;
        pieceCamera.SetActive(true);
        pieceCamera.GetComponent<PieceCamera>().Init();
        this.pieceMovement.UndoColoring();
        this.mouseSelection.UndoColoring();
    }
}
