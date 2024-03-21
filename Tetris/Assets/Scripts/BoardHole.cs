using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardHole : MonoBehaviour
{
    public Tilemap tilemap { get; private set;}
    public Piece activePiece { get; private set; }
    public Vector3Int spawnPosition;
    //private TetrominoData tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int[] cells { get; private set; }
    //public TetrominoData tetromino;
    public Piece piece;

    private void Awake()
    {

        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponent<Piece>();
        //piece.data.Initialize();
        
        // for(int i = 0; i < tetrominoes.Length; i++)

        // }
    }
   
    private void Update()
    {
       SpawnPiece();
    }
  
    public void SpawnPiece()
    {
       //int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = piece.data;
        activePiece.Initialize(this, spawnPosition, data);
        Set(activePiece);
    }


    public void Set(Piece piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);    
        }
    }


        public void Clear(Piece piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);    
        }
    }

    
}