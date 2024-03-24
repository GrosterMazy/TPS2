using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {
    public int sizeX;
    public int sizeZ;
    public float spacing;
    public int resourceCellsOnMap;// can be not even only if sizeX and sizeZ not even
    public bool infiniteResource;
    public int resourceAmountPerCell;
    public GameObject cellPrefab;

    public Vector3 invalidVector3;
    public Vector2 invalidVector2;

    public Transform background;

    public Vector3 cellSize;

    public List<List<GameObject>> cells;

    public Material resourceColor;

    private float halfX, halfZ;
    void Start() {
        this.invalidVector3 = new Vector3(-1, -1, -1);
        this.invalidVector2 = new Vector2(-1, -1);

        this.GenerateCells();
        this.PlaceResource();
    }

    private void GenerateCells() {
        this.cells = new List<List<GameObject>>();
        for (int x = 0; x < sizeX; x++) {
            this.cells.Add(new List<GameObject>());
            for (int z = 0; z < sizeZ; z++) {
                this.cells[x].Add(null);
            }
        }

        Renderer backgroundRenderer = cellPrefab.GetComponent<Renderer>();
        Renderer cellRenderer = cellPrefab.GetComponent<Renderer>();

        this.cellSize = cellRenderer.bounds.size;
        this.halfX = ((this.sizeX * this.cellSize.x) + ((this.sizeX-1) * this.spacing)) / 2;
        this.halfZ = ((this.sizeZ * this.cellSize.z) + ((this.sizeZ-1) * this.spacing)) / 2;

        this.background.position = new Vector3(0, -cellRenderer.bounds.size.y, 0);

        int xOnBoard = 0;
        int zOnBoard = 0;
        for (float x = -this.halfX + this.cellSize.x/2; x <= this.halfX; x += this.spacing + this.cellSize.x) {
            zOnBoard = 0;
            for (float z = -this.halfZ + this.cellSize.z/2; z <= this.halfZ; z += this.spacing + this.cellSize.z) {
                this.cells[xOnBoard][zOnBoard] = Instantiate(
                        this.cellPrefab,
                        new Vector3(x, -this.cellSize.y/2, z),
                        Quaternion.identity,
                        this.transform
                    );
                zOnBoard++;
            }
            xOnBoard++;
        }
    }

    private void PlaceResource() {
        int placed = 0;
        
        while (placed < (int)(this.resourceCellsOnMap/2)*2) {
            int x = Random.Range(0, this.cells.Count);
            int z = Random.Range(0, ((int)(this.cells[0].Count/2))+1);
            
            if (this.sizeX % 2 != 0 && this.sizeZ % 2 != 0
                && x == ((int)(this.cells.Count/2))
                && z == ((int)(this.cells[0].Count/2)))
                continue;
            
            GameObject cell = this.cells[x][z];
            GameObject cell2 = this.cells[this.cells.Count-1-x][this.cells[0].Count-1-z];

            if (cell.GetComponent<WithResource>() == null) {
                WithResource withResource = cell.AddComponent<WithResource>();
                if (this.infiniteResource) withResource.amount = -1;
                else withResource.amount = this.resourceAmountPerCell;

                // color cell
                cell.GetComponent<MeshRenderer>().material.color = this.resourceColor.color;

                placed++;

                WithResource withResource2 = cell2.AddComponent<WithResource>();
                if (this.infiniteResource) withResource2.amount = -1;
                else withResource2.amount = this.resourceAmountPerCell;

                // color cell
                cell2.GetComponent<MeshRenderer>().material.color = this.resourceColor.color;
                
                placed++;
            }
        }

        if (this.resourceCellsOnMap % 2 != 0
                && this.sizeX % 2 != 0
                && this.sizeZ % 2 != 0) {
            WithResource withResource = this.cells[((int)(this.cells.Count/2))][((int)(this.cells[0].Count/2))]
                .AddComponent<WithResource>();
            
            if (this.infiniteResource) withResource.amount = -1;
            else withResource.amount = this.resourceAmountPerCell;

            // color cell
            this.cells[((int)(this.cells.Count/2))][((int)(this.cells[0].Count/2))]
                .GetComponent<MeshRenderer>().material.color = this.resourceColor.color;
        }
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
    public Vector2 GetCoords(Vector3 position) {
        int x = 0;
        int z = 0;
        foreach (List<GameObject> row in this.cells) {
            z = 0;
            foreach (GameObject cell in row) {
                if (cell.transform.position == position)
                    return new Vector2(x, z);
                z++;
            }
            x++;
        }
        return this.invalidVector2;
    }
}
