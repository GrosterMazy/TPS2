using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAim : MonoBehaviour {
    public Transform aimObject;
    public Camera cameraLink;
    public float targetSkyDistance;
    void Update() {
        RaycastHit hit;
        Ray ray = this.cameraLink.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(ray, out hit))
            this.aimObject.position = hit.point;
        else this.aimObject.position = ray.GetPoint(targetSkyDistance);
    }
}
