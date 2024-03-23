using System.Numerics;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoardNext : MonoBehaviour
{
    public Tilemap tilemap { get; private set;}
    public PieceNext activePiece { get; private set; }
    public Vector3Int spawnPosition;
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
        SpawnPiece();   
    }


    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];
        activePiece.Initialize(boardNext, spawnPosition, data);
        Set(this.activePiece);
        
    }

    public void Set(PieceNext piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);    
        }
    }
   
   public void Clear(PieceNext piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);    
        }
    }
    

}
    


