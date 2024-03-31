using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Timeline;

public class PieceHole : MonoBehaviour
{
    public BoardHole board { get; private set;}
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float moveDelay = 0.2f;
    private float stepTime;
    private float lockTime;
    private float moveTime;
    public void Initializel(BoardHole board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if(this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }
        for(int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        
    }

 
 
}
