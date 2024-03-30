using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawn : MonoBehaviour {
    public Material spawnHighlightColor;
    public BoardGeneration board;
    public PieceManager pieceManager;
    public MouseSelection mouseSelection;
    public ActionButtons actionButtons;

    public GameObject wallPrefab;
    public GameObject minerPiecePrefab;
    public GameObject defenderPiecePrefab;
    public GameObject sniperPiecePrefab;
    public GameObject armorRemoverPrefab;

    public Stack<CellAndColor> spawnHighlighted;

    void Start() {
        this.spawnHighlighted = new Stack<CellAndColor>();
    }

    void Update() {
        if (this.actionButtons.choosingPlaceToSpawn) {
            if (this.mouseSelection.selected.GetComponent<PieceModel>().parent.actionsRemain > 0)
                ColorPlaces();
            else this.UndoColoring();
        }
        else this.UndoColoring();
    }

    public void ColorPlaces() {
        DicePiece parent = this.mouseSelection.selected.GetComponent<PieceModel>().parent;
        foreach (Vector2 place in parent.Moves()) {
            // place in bounds of board
            if (parent.boardX+place.x >= 0 && parent.boardX+place.x < this.board.sizeX
                    && parent.boardZ+place.y >= 0 && parent.boardZ+place.y < this.board.sizeZ) {
                
                bool found = false;
                foreach (Piece piece in this.pieceManager.pieces)
                    if (piece.boardX == parent.boardX+place.x && piece.boardZ == parent.boardZ+place.y) {
                        found = true;
                        break;
                    }
                // no piece in this spot
                if (!found) {
                    this.spawnHighlighted.Push(new CellAndColor(
                        this.board.cells[parent.boardX+(int)place.x][parent.boardZ+(int)place.y]
                            .GetComponent<MeshRenderer>().material.color,
                        new Vector2(parent.boardX+place.x, parent.boardZ+place.y)
                    ));
                    this.board.cells[parent.boardX+(int)place.x][parent.boardZ+(int)place.y]
                        .GetComponent<MeshRenderer>().material.color = this.spawnHighlightColor.color;
                }
            }
        }
    }
    public void UndoColoring() {
        while (this.spawnHighlighted.Count > 0) {
                CellAndColor cellAndColor = this.spawnHighlighted.Pop();
                this.board.cells[(int)cellAndColor.position.x][(int)cellAndColor.position.y]
                    .GetComponent<MeshRenderer>().material.color = cellAndColor.color;
        }
    }

    public void SpawnPiece(Vector3 positionCell) {
        DicePiece parent = this.mouseSelection.selected.GetComponent<PieceModel>().parent;

        int newX = (int)this.board.GetCoords(positionCell).x;
        int newZ = (int)this.board.GetCoords(positionCell).y;

        GameObject piece;
        switch (parent.power) {
            case 1:
                piece = Instantiate(this.minerPiecePrefab);
                break;
            case 2:
                // TODO: spawn Wall(now i don't added this because king can trap himself in walls)
                piece = Instantiate(this.minerPiecePrefab); 
                break;
            case 3:
                piece = Instantiate(this.defenderPiecePrefab);
                break;
            case 4:
                piece = Instantiate(this.sniperPiecePrefab);  // TODO: choose to spawn Sniper or Grenader
                break;
            case 5:
                piece = Instantiate(this.armorRemoverPrefab);
                break;
            case 6:
                piece = Instantiate(this.minerPiecePrefab); // TODO: spawn Mover
                break;
            default:
                piece = null;
                break;
        }

        Piece pieceComponent = piece.GetComponent<Piece>();
        pieceComponent.team = parent.team;
        pieceComponent.boardX = newX;
        pieceComponent.boardZ = newZ;
        
        this.pieceManager.InitializePiece(pieceComponent);
        this.pieceManager.pieces.Add(pieceComponent);

        parent.actionsRemain--;
        parent.resourceAmount--;
        parent.movesRemain = 0;
        
        actionButtons.choosingPlaceToSpawn = false;
    }
}
