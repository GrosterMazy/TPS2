using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {
    public int sizeX;
    public int sizeZ;
    public float spacing;
    public GameObject cellPrefab;

    public Transform background;

    void Start() {
        Renderer backgroundRenderer = cellPrefab.GetComponent<Renderer>();
        Renderer cellRenderer = cellPrefab.GetComponent<Renderer>();

        this.background.position = new Vector3(0, -cellRenderer.bounds.size.y, 0);

        float halfX = ((this.sizeX * cellRenderer.bounds.size.x) + ((this.sizeX-1) * this.spacing)) / 2;
        float halfZ = ((this.sizeZ * cellRenderer.bounds.size.z) + ((this.sizeZ-1) * this.spacing)) / 2;

        for (float x = -halfX + cellRenderer.bounds.size.x/2; x <= halfX; x += this.spacing + cellRenderer.bounds.size.x)
            for (float z = -halfZ + cellRenderer.bounds.size.z/2; z <= halfZ; z += this.spacing + cellRenderer.bounds.size.z)
                Instantiate(this.cellPrefab, new Vector3(x, -cellRenderer.bounds.size.y/2, z), Quaternion.identity, this.transform);
    }
}
