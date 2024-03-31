using System.Collections;
using System.Numerics;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.Collections;

public class BoardNext : MonoBehaviour
{
    public Tilemap tilemap { get; private set;}
    public PieceNext activePiece { get; private set; }
    public List<TetrominoData> pieceNexts;
    private Vector3Int spawnPosition = new Vector3Int(9, 6, 0);
    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int[] cells { get; private set; }

    [SerializeField] BoardNext boardNext;
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<PieceNext>();
        for(int i = 0; i < tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }  
    }

    private void Start()
    {
        for(int i = 0; i < 5; i++){
            int random = Random.Range(0, tetrominoes.Length);
            TetrominoData data = tetrominoes[random];
            pieceNexts.Add(data);
        }
        SpawnPiece();   
    }


    public void SpawnPiece()
    {
        Vector3Int savepositon = spawnPosition;
        for(int i = 4; i >= 0; i--){
        TetrominoData data = pieceNexts[i];
        activePiece.Initialize(boardNext, savepositon, data);
        Set(this.activePiece);
        savepositon.y -= 3;
        }
        // int random = Random.Range(0, tetrominoes.Length);
        // TetrominoData data = tetrominoes[random];
        // activePiece.Initialize(boardNext, spawnPosition, data);
        // Set(this.activePiece);
    }

    public void Set(PieceNext piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);    
        }
    }
   
   
   public void Clear()
    {
        for(int i = 7; i <= 12; i++)
        {
            for(int j = 7; j >= -9; j--)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                this.tilemap.SetTile(tilePosition, null); 
            }              
        }
    }
    

}
    


