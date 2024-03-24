using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieceManager : MonoBehaviour {
    public BoardGeneration board;
    public GameObject boardCamera;
    public TextMeshProUGUI turnText;

    public List<TeamInfo> teams;

    public List<Piece> pieces;

    private int _turnCount = 0;

    void Start() {
        foreach (Piece piece in this.pieces) {
            // set team color
            DicePiece dicePiece = piece.gameObject.GetComponent<DicePiece>();
            if (dicePiece != null)
                dicePiece.icon.material.color = this.teams[piece.team].material.color;
            
            // set boardCamera link
            ShootingPiece shootingPiece = piece.gameObject.GetComponent<ShootingPiece>();
            if (shootingPiece != null)
                shootingPiece.localCameraLink.GetComponent<PieceCamera>().boardCamera = this.boardCamera;
        }
        // set turn text
        this.turnText.text = this.teams[this._turnCount % this.teams.Count].turnText;
        this.turnText.color = this.teams[this._turnCount % this.teams.Count].material.color;
    }

    void Update() {
        foreach (Piece piece in this.pieces) {
            // place piece
            piece.transform.position = this.board.GetPlace(piece.boardX, piece.boardZ, piece.height);
        }
    }

    public bool TurnOf(Piece piece) {
        return this._turnCount % this.teams.Count == piece.team;
    }

    public void NextTurn() {
        // restore piece moves
        foreach (Piece piece in this.pieces)
            if (this.TurnOf(piece)) {
                DicePiece dicePiece = piece.gameObject.GetComponent<DicePiece>();
                if (dicePiece != null)
                    dicePiece.movesRemain = dicePiece.moves;
            }
        
        // add turn count
        this._turnCount++;

        // set turn text
        this.turnText.text = this.teams[this._turnCount % this.teams.Count].turnText;
        this.turnText.color = this.teams[this._turnCount % this.teams.Count].material.color;
    }
}
