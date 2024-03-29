using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] public class TeamInfo {
    public Material material;
    public string turnText;
    public string winText;
    public Vector3 boardCameraBasePosition;
    public float boardCameraZoom;
    public Quaternion boardCameraRotation;
    public int teamPieceYRotation; // number of steps by 90. can be 0, 1, 2 or 3
    public List<KingPiece> teamKings;

    public TeamInfo(Material material, string turnText, Vector3 boardCameraBasePosition,
            float boardCameraZoom, Quaternion boardCameraRotation, int teamPieceYRotation,
            List<KingPiece> teamKings) {
        this.material = material;
        this.turnText = turnText;
        this.boardCameraBasePosition = boardCameraBasePosition;
        this.boardCameraZoom = boardCameraZoom;
        this.boardCameraRotation = boardCameraRotation;
        this.teamPieceYRotation = teamPieceYRotation;
        this.teamKings = teamKings;
    }
}
