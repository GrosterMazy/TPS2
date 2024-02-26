using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {
    public int sizeX;
    public int sizeZ;
    public float spacing;
    public GameObject cellPrefab;

    public Transform background;

    public Vector3 cellSize;

    void Start() {
        Renderer backgroundRenderer = cellPrefab.GetComponent<Renderer>();
        Renderer cellRenderer = cellPrefab.GetComponent<Renderer>();
        this.cellSize = cellRenderer.bounds.size;

        this.background.position = new Vector3(0, -cellRenderer.bounds.size.y, 0);

        float halfX = ((this.sizeX * this.cellSize.x) + ((this.sizeX-1) * this.spacing)) / 2;
        float halfZ = ((this.sizeZ * this.cellSize.z) + ((this.sizeZ-1) * this.spacing)) / 2;

        for (float x = -halfX + this.cellSize.x/2; x <= halfX; x += this.spacing + this.cellSize.x)
            for (float z = -halfZ + this.cellSize.z/2; z <= halfZ; z += this.spacing + this.cellSize.z)
                Instantiate(this.cellPrefab, new Vector3(x, -this.cellSize.y/2, z), Quaternion.identity, this.transform);
    }
}
