using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceModel : MonoBehaviour {
    public DicePiece parent;

    public RaycastHit[] hits;

    void Start() {
        this.hits = new RaycastHit[6];
        for (int i = 0; i < this.hits.Length; i++)
            this.hits[i] = new RaycastHit();
    }

    void Update() {
        Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out this.hits[0]);    // 1
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue); // z

        Physics.Raycast(new Ray(this.transform.position, this.transform.up), out this.hits[1]);         // 2
        Debug.DrawRay(this.transform.position, this.transform.up, Color.green); // y

        Physics.Raycast(new Ray(this.transform.position, -this.transform.right), out this.hits[2]);     // 3
        Debug.DrawRay(this.transform.position, -this.transform.right);

        Physics.Raycast(new Ray(this.transform.position, this.transform.right), out this.hits[3]);      // 4
        Debug.DrawRay(this.transform.position, this.transform.right, Color.red); // x

        Physics.Raycast(new Ray(this.transform.position, -this.transform.up), out this.hits[4]);        // 5
        Debug.DrawRay(this.transform.position, -this.transform.up);

        Physics.Raycast(new Ray(this.transform.position, -this.transform.forward), out this.hits[5]);   // 6
        Debug.DrawRay(this.transform.position, -this.transform.forward);
    }
}