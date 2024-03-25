using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public GameObject model;

    public int boardX;
    public int boardZ;

    public int actions;
    public int actionsRemain;

    public int team;

    public float height;

    protected virtual void Start() {
        this.height = this.model.GetComponent<Renderer>().bounds.size.y;
        this.actionsRemain = this.actions;
    }
}
