using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public GameObject model;
    public GameObject localCameraLink;
    public MeshRenderer icon;

    public int boardX;
    public int boardZ;

    public int moves;
    public int movesRemain;
    public int maxResourceAmount;
    public int resourceAmount;

    public int team;

    public float height;
    public int power;

    void Start() {
        this.height = this.model.GetComponent<Renderer>().bounds.size.y;
        this.movesRemain = this.moves;
    }

    void Update() {
        PieceModel pieceModel = this.model.GetComponent<PieceModel>();
        for (int i = 0; i < pieceModel.hits.Length; i++)
            if (pieceModel.hits[i].collider != null
                    && pieceModel.hits[i].transform.GetComponent<TopSideChecker>() != null) {
                this.power = i+1;
                break;
            }
    }

    public List<Vector2> Moves() {
        List<Vector2> list = new List<Vector2>();
        list.Add(new Vector2(-1, 0));
        list.Add(new Vector2(1, 0));
        list.Add(new Vector2(0, -1));
        list.Add(new Vector2(0, 1));
        return list;
    }
}
