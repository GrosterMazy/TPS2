using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHighlight : MonoBehaviour {

    public Camera cameraLink;
    public Material highlightColor;

    public Transform highlighted;

    private Color _highlightedOldColor;

    void Update() {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(this.cameraLink.ScreenPointToRay(Input.mousePosition), out hit);

        // return color to last frame hightlight
        if (this.highlighted != null) { 
            this.highlighted.GetComponent<MeshRenderer>().material.color = this._highlightedOldColor;
            this.highlighted = null;
        }

        // highlight
        if (hasHit && hit.transform.CompareTag("Selectable") && !EventSystem.current.IsPointerOverGameObject()) {
            this._highlightedOldColor = hit.transform.GetComponent<MeshRenderer>().material.color;
            hit.transform.GetComponent<MeshRenderer>().material.color = this.highlightColor.color;
            this.highlighted = hit.transform;
        }
    }
    
    public void UndoColoring() {
        if (this.highlighted != null) { 
            this.highlighted.GetComponent<MeshRenderer>().material.color = this._highlightedOldColor;
            this.highlighted = null;
        }
    }
}
