using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePiece : Piece {
    public MeshRenderer icon;

    public PieceDataCanvas pieceDataCanvas;

    public Transform currentCamera;

    public int moves;
    public int movesRemain;

    public int maxResourceAmount;
    public int resourceAmount;

    public int power;

    public int yRotation; // number of steps by 90. can be 0, 1, 2 or 3

    public int shieldPower;

    private RectTransform _resourceTextRectTransform;

    protected override void Start() {
        base.Start();
        this.movesRemain = this.moves;
    }

    protected virtual void Update() {
        PieceModel pieceModel = this.model.GetComponent<PieceModel>();
        for (int i = 0; i < pieceModel.hits.Length; i++)
            if (pieceModel.hits[i].collider != null
                    && pieceModel.hits[i].transform.GetComponent<TopSideChecker>() != null) {
                this.power = i+1;
                break;
            }
        // set text
        this.pieceDataCanvas.resouceText.text = this.resourceAmount.ToString();
        this.pieceDataCanvas.shieldText.text = this.shieldPower.ToString();
        this.pieceDataCanvas.transform.rotation = Quaternion.LookRotation(
            this.pieceDataCanvas.transform.position - this.currentCamera.position
        );
    }
    
    public virtual List<Vector2> Moves() {
        List<Vector2> list = new List<Vector2>();
        list.Add(new Vector2(-1, 0));
        list.Add(new Vector2(1, 0));
        list.Add(new Vector2(0, -1));
        list.Add(new Vector2(0, 1));
        return list;
    }
}
