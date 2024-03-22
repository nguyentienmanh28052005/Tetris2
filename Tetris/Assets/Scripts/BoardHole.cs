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
    //private TetrominoData tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int[] cells { get; private set; }
    //public TetrominoData tetromino;
    public Piece piece;
    static int cnt = 0;
    TetrominoData data;

    

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<PieceHole>();
        
    }

    private void Start()
    {
        this.data = piece.data;
    }
   
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.X)){
            if(cnt != 0) 
            {
                Clear(activePiece);
            }
            SpawnPiece();cnt=1;
       }
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