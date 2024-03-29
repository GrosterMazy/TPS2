using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieceManager : MonoBehaviour {
    public string drawText;
    public BoardGeneration board;
    public GameObject boardCamera;
    public GameObject mouseSelection;
    public TextMeshProUGUI turnText;

    public List<TeamInfo> teams;

    public List<Piece> pieces;

    public GameObject inGameUI;
    public GameObject endGameUI;
    public TextMeshProUGUI endGameText;

    private bool _sandboxMode;
    private int _turnCount = 0;
    private BoardCamera _boardCameraComponent;
    private MouseSelection _mouseSelectionComponent;

    void Start() {
        if (this.teams.Count == 1) {
            this._sandboxMode = true;
        }

        this._boardCameraComponent = this.boardCamera.GetComponent<BoardCamera>();
        this._mouseSelectionComponent = this.mouseSelection.GetComponent<MouseSelection>();

        // set turn text
        this.turnText.text = this.teams[this._turnCount % this.teams.Count].turnText;
        this.turnText.color = this.teams[this._turnCount % this.teams.Count].material.color;

        // place board camera
        this._boardCameraComponent.basePosition = this.teams[this._turnCount % this.teams.Count].boardCameraBasePosition;
        this._boardCameraComponent.currentZoom = this.teams[this._turnCount % this.teams.Count].boardCameraZoom;
        this.boardCamera.transform.rotation = this.teams[this._turnCount % this.teams.Count].boardCameraRotation;

        foreach (Piece piece in this.pieces) {
            this.InitializePiece(piece);
        }
    }

    void Update() {
        foreach (Piece piece in this.pieces) {
            // place piece
            piece.transform.position = this.board.GetPlace(piece.boardX, piece.boardZ, piece.height);
        }
        // update team info
        this.teams[this._turnCount % this.teams.Count].boardCameraBasePosition = this._boardCameraComponent.basePosition;
        this.teams[this._turnCount % this.teams.Count].boardCameraZoom = this._boardCameraComponent.currentZoom;
        this.teams[this._turnCount % this.teams.Count].boardCameraRotation = this.boardCamera.transform.rotation;

        // remove teams without kings
        int i = 0;
        while (i < this.teams.Count) {
            if (this.teams[i].teamKings.Count == 0) {
                // remove pieces from board
                int j = 0;
                while (j < this.pieces.Count) {
                    if (this.pieces[j].team == i) {
                        this.pieces.RemoveAt(j);
                        continue;
                    }
                    j++;
                }
                // remove team
                this.teams.RemoveAt(i);
                // update team indexes
                foreach (Piece piece in this.pieces)
                    if (piece.team > i)
                        piece.team--;
                
                continue;
            }
            i++;
        }

        // last team won
        if (this.teams.Count == 1 && !this._sandboxMode) {
            this.inGameUI.SetActive(false);
            this.endGameUI.SetActive(true);
            this.endGameText.text = this.teams[0].winText;
            this.endGameText.color = this.teams[0].material.color;

            foreach (Piece piece in this.pieces) {
                ShootingPiece shootingPiece = piece.gameObject.GetComponent<ShootingPiece>();
                if (shootingPiece != null) {
                    shootingPiece.localCameraLink.GetComponent<PieceCamera>().ReturnToBoardCamera();
                    piece.gameObject.SetActive(false);
                }
            }
            this._boardCameraComponent.enabled = false;
            this._mouseSelectionComponent.enabled = false;
        }
        // draw
        if (this.teams.Count == 0) {
            this.inGameUI.SetActive(false);
            this.endGameUI.SetActive(true);
            this.endGameText.text = drawText;
        }
    }
    public void InitializePiece(Piece piece) {
        piece.pieceManager = this;

        DicePiece dicePiece = piece.gameObject.GetComponent<DicePiece>();
        if (dicePiece != null) {
            // set team color
            dicePiece.icon.material.color = this.teams[piece.team].material.color;
            dicePiece.pieceDataCanvas.resouceText.color = this.teams[piece.team].material.color;
            dicePiece.pieceDataCanvas.shieldText.color = this.teams[piece.team].material.color;
            dicePiece.pieceDataCanvas.shieldImage.color = this.teams[piece.team].material.color;
            // set boardCamera links
            dicePiece.currentCamera = this.boardCamera.transform;
            // set y rotation;
            dicePiece.yRotation = this.teams[piece.team].teamPieceYRotation;
            piece.transform.RotateAround(
                piece.transform.position,
                Vector3.up,
                90*dicePiece.yRotation
            );
        }

        ShootingPiece shootingPiece = piece.gameObject.GetComponent<ShootingPiece>();
        if (shootingPiece != null) {
            PieceCamera pieceCamera = shootingPiece.localCameraLink.GetComponent<PieceCamera>();
            pieceCamera.boardCamera = this.boardCamera;
            pieceCamera.pieceManager = this;
        }

        KingPiece kingPiece = piece.gameObject.GetComponent<KingPiece>();
        if (kingPiece != null)
            this.teams[piece.team].teamKings.Add(kingPiece);
    }

    public bool TurnOf(Piece piece) {
        return this._turnCount % this.teams.Count == piece.team;
    }

    public void NextTurn() {
        // restore piece moves and actions
        foreach (Piece piece in this.pieces)
            if (this.TurnOf(piece)) {
                DicePiece dicePiece = piece.gameObject.GetComponent<DicePiece>();
                if (dicePiece != null) {
                    dicePiece.movesRemain = dicePiece.moves;
                    dicePiece.actionsRemain = dicePiece.actions;
                }
            }
        
        // add turn count
        this._turnCount++;

        // place board camera
        this._boardCameraComponent.basePosition = this.teams[this._turnCount % this.teams.Count].boardCameraBasePosition;
        this._boardCameraComponent.currentZoom = this.teams[this._turnCount % this.teams.Count].boardCameraZoom;
        this.boardCamera.transform.rotation = this.teams[this._turnCount % this.teams.Count].boardCameraRotation;

        // set turn text
        this.turnText.text = this.teams[this._turnCount % this.teams.Count].turnText;
        this.turnText.color = this.teams[this._turnCount % this.teams.Count].material.color;
    }
}
