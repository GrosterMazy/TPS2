using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public GameObject model;
    public int boardX;
    public int boardZ;
    public float height;
    void Start() {
        this.height = this.model.GetComponent<Renderer>().bounds.size.y;
    }
}
