using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCamera : MonoBehaviour {
    public BoardGeneration board;
    public float distanceFromBoard;
    public float zoomSpeed;
    public float minHeightAboveBoard;
    public float maxHeightAboveBoard;
    void Update() {
        float halfZ = ((this.board.sizeZ * this.board.cellSize.z) + ((this.board.sizeZ-1) * this.board.spacing)) / 2;
        this.transform.position = new Vector3(
            0,
            Mathf.Clamp(this.transform.position.y + this.zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel"), this.minHeightAboveBoard, this.maxHeightAboveBoard),
            -halfZ-this.distanceFromBoard
        );

        this.transform.LookAt(Vector3.zero);
    }
}
