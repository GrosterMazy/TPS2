using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCamera : MonoBehaviour {
    public Transform piece;
    public GameObject boardCamera;
    public Transform pieceIcon;
    public Camera cameraLink;
    public MouseHighlight mouseHighlight;

    public float rotationSpeed;
    public float maxVerticalAngle;
    public float minVerticalAngle;

    private float _newRotX;

    void Update() {
        this.UpdateRotation();

        // copy rotation to icon
        this.pieceIcon.localEulerAngles = this.transform.localEulerAngles;

        // Exit piece
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.Deinit();
            this.gameObject.SetActive(false);
            this.boardCamera.SetActive(true);
        }
    }

    private void UpdateRotation() {
        // mouse x
        this.piece.localEulerAngles = new Vector3(
            this.piece.localEulerAngles.x,
            this.piece.localEulerAngles.y + this.rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"),
            0
        );
        // mouse y
        this._newRotX = this.transform.localEulerAngles.x - this.rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
        if (this._newRotX < this.maxVerticalAngle || this._newRotX > 360+this.minVerticalAngle)
            this.transform.localEulerAngles = new Vector3(
                this._newRotX,
                this.transform.localEulerAngles.y, 0
            );
    }

    public void Init() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Deinit() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.piece.localEulerAngles = new Vector3(this.piece.localEulerAngles.x, 0, this.piece.localEulerAngles.z);
        this.mouseHighlight.UndoColoring();
    }
}
