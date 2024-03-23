using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceNext : MonoBehaviour
{
    public BoardNext board { get; private set;}
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position;

    public void Initialize(BoardNext board, Vector3Int position, TetrominoData data)
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
}
