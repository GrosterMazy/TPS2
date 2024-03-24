using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour {
    public BoardGeneration board;
    public Material moveHighlightColor;
    public MouseSelection mouseSelection;
    public PieceManager pieceManager;

    public Stack<CellAndColor> moveHighlighted;

    void Start() {
        this.moveHighlighted = new Stack<CellAndColor>();
    }

    void Update() {
        if (this.mouseSelection.selected != null) {
            PieceModel pieceModel = this.mouseSelection.selected.GetComponent<PieceModel>();
            if (pieceModel != null && pieceManager.TurnOf(pieceModel.parent)
                    && pieceModel.parent.movesRemain > 0)
                ColorMoves();
            else this.UndoColoring();
        }
        else this.UndoColoring();
    }
    public void ColorMoves() {
        DicePiece parent = this.mouseSelection.selected.GetComponent<PieceModel>().parent;
        foreach (Vector2 move in parent.Moves()) {
            // move in bounds of board
            if (parent.boardX+move.x >= 0 && parent.boardX+move.x < this.board.sizeX
                    && parent.boardZ+move.y >= 0 && parent.boardZ+move.y < this.board.sizeZ) {
                
                bool found = false;
                foreach (Piece piece in this.pieceManager.pieces)
                    if (piece.boardX == parent.boardX+move.x && piece.boardZ == parent.boardZ+move.y) {
                        found = true;
                        break;
                    }
                // no piece in this spot
                if (!found) {
                    this.moveHighlighted.Push(new CellAndColor(
                        this.board.cells[parent.boardX+(int)move.x][parent.boardZ+(int)move.y]
                            .GetComponent<MeshRenderer>().material.color,
                        new Vector2(parent.boardX+move.x, parent.boardZ+move.y)
                    ));
                    this.board.cells[parent.boardX+(int)move.x][parent.boardZ+(int)move.y]
                        .GetComponent<MeshRenderer>().material.color = this.moveHighlightColor.color;
                }
            }
        }
    }
    public void UndoColoring() {
        while (this.moveHighlighted.Count > 0) {
                CellAndColor cellAndColor = this.moveHighlighted.Pop();
                this.board.cells[(int)cellAndColor.position.x][(int)cellAndColor.position.y]
                    .GetComponent<MeshRenderer>().material.color = cellAndColor.color;
        }
    }

    public void MakeMove(Vector3 destinationCell) {
        DicePiece parent = this.mouseSelection.selected.GetComponent<PieceModel>().parent;
        parent.movesRemain--;

        int newX = (int)this.board.GetCoords(destinationCell).x;
        int newZ = (int)this.board.GetCoords(destinationCell).y;

        Vector3 xDirection = (Vector3.right * (newX-parent.boardX)) / Mathf.Abs(newX-parent.boardX);
        for (int i = 0; i < Mathf.Abs(newX-parent.boardX); i++)
            this.mouseSelection.selected.RotateAround(
                this.mouseSelection.selected.position,
                Vector3.Cross(Vector3.up, xDirection),
                90
            );
        
        Vector3 zDirection = (Vector3.forward * (newZ-parent.boardZ)) / Mathf.Abs(newZ-parent.boardZ);
        for (int i = 0; i < Mathf.Abs(newZ-parent.boardZ); i++)
            this.mouseSelection.selected.RotateAround(
                this.mouseSelection.selected.position,
                Vector3.Cross(Vector3.up, zDirection),
                90
            );
        
        //this.mouseSelection.selected.eulerAngles -= new Vector3(0, 0, 90*(newX-parent.boardX));
        //this.mouseSelection.selected.eulerAngles += new Vector3(90*(newZ-parent.boardZ), 0, 0);

        parent.boardX = newX;
        parent.boardZ = newZ;
    }
}
