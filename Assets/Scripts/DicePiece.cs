using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePiece : Piece {
    public MeshRenderer icon;

    public int moves;
    public int movesRemain;

    public int maxResourceAmount;
    public int resourceAmount;

    public int power;

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
