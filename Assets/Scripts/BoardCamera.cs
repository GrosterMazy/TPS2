using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCamera : MonoBehaviour {
    public BoardGeneration board;

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    
    public float moveSpeed;

    public float rotationSpeed;
    public float maxVerticalAngle;
    public float minVerticalAngle;

    public Camera cameraLink;

    private float _newRotX;
    private float _currentZoom = 0;
    private Vector3 _basePosition;

    void Start() {
        this._basePosition = this.transform.localPosition;
    }

    void Update() {
        // camera movement
        if (Input.GetMouseButton(1))
            this._basePosition +=
                - (new Vector3(this.transform.right.x, 0, this.transform.right.z)).normalized
                    * this.moveSpeed * Time.deltaTime * Input.GetAxis("Mouse X")
                - Vector3.Cross(
                        (new Vector3(this.transform.right.x, 0, this.transform.right.z)).normalized,
                        new Vector3(0, 1, 0)
                    )
                    * this.moveSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
        // camera rotation
        else if (Input.GetMouseButton(2)) {
            this.transform.localEulerAngles = new Vector3(
                this.transform.localEulerAngles.x,
                this.transform.localEulerAngles.y + this.rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"),
                0
            );
            this._newRotX = this.transform.localEulerAngles.x - this.rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
            if (this._newRotX < this.maxVerticalAngle || this._newRotX > 360+this.minVerticalAngle)
                this.transform.localEulerAngles = new Vector3(
                    this._newRotX,
                    this.transform.localEulerAngles.y, 0
                );
        }
        // camera zoom
        if (this.minZoom <= this._currentZoom && this._currentZoom <= this.maxZoom)
            this._currentZoom -= this.zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
        else this._currentZoom = Mathf.Clamp(this._currentZoom, this.minZoom, this.maxZoom);

        // applying
        this.transform.localPosition = this._basePosition - this.transform.forward * this._currentZoom;
    }

}
