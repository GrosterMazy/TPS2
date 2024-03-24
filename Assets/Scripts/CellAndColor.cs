using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAndColor {
    public Color color;
    public Vector2 position;

    public CellAndColor(Color color, Vector2 position) {
        this.color = color;
        this.position = position;
    }
}
