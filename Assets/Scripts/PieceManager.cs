using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {
    public BoardGeneration board;

    public List<Piece> pieces;

    void Update() {
        foreach (Piece piece in this.pieces)
            piece.transform.position = this.board.GetPlace(piece.boardX, piece.boardZ, piece.height);
    }
}
