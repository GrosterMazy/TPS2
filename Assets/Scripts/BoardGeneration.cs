using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {
    public int sizeX;
    public int sizeZ;
    public float spacing;
    public GameObject cellPrefab;

    public Vector3 invalidVector3;

    public Transform background;

    public Vector3 cellSize;


    private float halfX, halfZ;
    void Start() {
        invalidVector3 = new Vector3(-1, -1, -1);

        Renderer backgroundRenderer = cellPrefab.GetComponent<Renderer>();
        Renderer cellRenderer = cellPrefab.GetComponent<Renderer>();

        this.cellSize = cellRenderer.bounds.size;
        this.halfX = ((this.sizeX * this.cellSize.x) + ((this.sizeX-1) * this.spacing)) / 2;
        this.halfZ = ((this.sizeZ * this.cellSize.z) + ((this.sizeZ-1) * this.spacing)) / 2;

        this.background.position = new Vector3(0, -cellRenderer.bounds.size.y, 0);

        for (float x = -this.halfX + this.cellSize.x/2; x <= this.halfX; x += this.spacing + this.cellSize.x)
            for (float z = -this.halfZ + this.cellSize.z/2; z <= this.halfZ; z += this.spacing + this.cellSize.z)
                Instantiate(this.cellPrefab, new Vector3(x, -this.cellSize.y/2, z), Quaternion.identity, this.transform);
    }

    public Vector3 GetPlace(int x, int z, float height) {
        if (x >= 0 && x <= this.sizeX-1 && z >= 0 && z <= this.sizeZ-1)
            return new Vector3(
                -this.halfX + this.cellSize.x/2 + (this.spacing + this.cellSize.x) * x,
                height/2,
                -this.halfZ + this.cellSize.z/2 + (this.spacing + this.cellSize.z) * z
            );
        return this.invalidVector3;
    }
}
