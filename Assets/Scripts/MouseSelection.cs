using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MouseSelection : MonoBehaviour
{
    public Camera cameraLink;
    public Material highlightColor;
    public Material selectionColor;
    public PieceMovement pieceMovement;
    public BoardGeneration board;
    public PieceManager pieceManager;

    public Transform highlighted;
    public Transform selected;

    private Color _highlightedOldColor;
    private Color _selectedOldColor;

    public void Update() {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(this.cameraLink.ScreenPointToRay(Input.mousePosition), out hit);

        this.UpdateHighlight(hit, hasHit);
        this.UpdateSelection(hit);
    }

    void UpdateHighlight(RaycastHit hit, bool hasHit) {
        // return color to last frame hightlight
        if (this.highlighted != null) { 
            this.highlighted.GetComponent<MeshRenderer>().material.color = this._highlightedOldColor;
            this.highlighted = null;
        }

        // highlight
        if (hasHit && hit.transform.CompareTag("Selectable") && hit.transform != this.selected
                && !EventSystem.current.IsPointerOverGameObject()) {
            if (this.selected != null) {
                PieceModel pieceModel = this.selected.GetComponent<PieceModel>();
                if (pieceModel != null && pieceManager.TurnOf(pieceModel.parent)
                        && pieceModel.parent.movesRemain > 0) {
                    this.pieceMovement.UndoColoring();
                    this._highlightedOldColor = hit.transform.GetComponent<MeshRenderer>().material.color;
                    this.pieceMovement.ColorMoves();
                }
                else this._highlightedOldColor = hit.transform.GetComponent<MeshRenderer>().material.color;
            }
            else this._highlightedOldColor = hit.transform.GetComponent<MeshRenderer>().material.color;
            hit.transform.GetComponent<MeshRenderer>().material.color = this.highlightColor.color;
            this.highlighted = hit.transform;
        }
    }
    void UpdateSelection(RaycastHit hit) {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (this.highlighted != null) {
                
                if (this.selected != null) {
                    // return old color to old selection
                    this.selected.GetComponent<MeshRenderer>().material.color = this._selectedOldColor;

                    if (this.selected.GetComponent<PieceModel>() != null) {

                        // if we click on potential move: make it
                        foreach (CellAndColor cellAndColor in this.pieceMovement.moveHighlighted) {
                            Vector3 cellCoords = this.board.GetPlace((int)cellAndColor.position.x, (int)cellAndColor.position.y, 0);
                            if (Math.Round(cellCoords.x, 4) == Math.Round(hit.transform.position.x, 4)
                                    && Math.Round(cellCoords.z, 4) == Math.Round(hit.transform.position.z, 4)) {
                                this.pieceMovement.MakeMove(hit.transform.position);
                                break;
                            }
                        }
                        // undo cells coloring if old selection was piece
                        this.pieceMovement.UndoColoring();
                    }       
                }

                // selection
                this.selected = hit.transform;

                this._selectedOldColor = this._highlightedOldColor; // copy old color from hightlight
                this.selected.GetComponent<MeshRenderer>().material.color = this.selectionColor.color;
                
                this.highlighted = null;
            }
            // return old color to selection when click outside
            else if (this.selected != null) {
                this.selected.GetComponent<MeshRenderer>().material.color = this._selectedOldColor;
                this.selected = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && this.selected != null) {
            this.selected.GetComponent<MeshRenderer>().material.color = this._selectedOldColor;
            this.selected = null;
        }
    }

    public void UndoColoring() {
        if (this.selected != null) {
            this.selected.GetComponent<MeshRenderer>().material.color = this._selectedOldColor;
            this.selected = null;
        }
        if (this.highlighted != null) { 
            this.highlighted.GetComponent<MeshRenderer>().material.color = this._highlightedOldColor;
            this.highlighted = null;
        }
    }
}