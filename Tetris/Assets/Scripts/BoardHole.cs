using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardHole : MonoBehaviour
{
    public Tilemap tilemap { get; private set;}
    public PieceHole activePiece { get; private set; }
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int[] cells { get; private set; }
    public Piece piece;




    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<PieceHole>();
        
    }

    public void SpawnPiece()
    {
       //int random = Random.Range(0, tetrominoes.Length); 
        TetrominoData data = piece.data;
        activePiece.Initializel(this, spawnPosition, data);
        Set(activePiece);
    }


    public void Set(PieceHole piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);    
        }
    }


    public void Clear(PieceHole piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);    
        }
    }

    
}