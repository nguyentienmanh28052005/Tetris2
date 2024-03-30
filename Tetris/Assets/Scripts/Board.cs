using System.Numerics;
using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set;}
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public Diem _diem;
    public Level _level;

    [SerializeField] BoardHole boardHole;
    //[SerializeField] List<TetrominoData> pieceNext;
    [SerializeField] BoardNext boardNext;

    public int diem;

    public int level;

    int x = 500;




    int cnt = 0;
    public RectInt Bounds{
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

  
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for(int i = 0; i < tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }  
    }

    private void Start()
    {
        SpawnPiece();   
        _level.UpdateLevel();
    }

    private void Update(){
        Level();
        Swap();
    }

    
    private void Level()
    {
        if(diem >= x){
            x+=500;
            level+=1;
            _level.UpdateLevel();
            activePiece.stepDelay  -= activePiece.stepDelay*0.25f;
        }
    }

    private void Swap()
    {
        if(Input.GetMouseButtonDown(1))
            {
                if(cnt != 0)
                {
                TetrominoData saveData = activePiece.data;
                Clear(activePiece);
                if(IsValidPositionHole(boardHole.activePiece, activePiece.position))
                {
                    activePiece.Initialize(this, activePiece.position, boardHole.activePiece.data);
                }else{
                    return;
                    // activePiece.position.x+=2;
                    // activePiece.Initialize(this, activePiece.position, boardHole.activePiece.data); 
                }              
                boardHole.Clear(boardHole.activePiece);
                boardHole.activePiece.Initializel(boardHole, boardHole.spawnPosition, saveData);
                boardHole.Set(boardHole.activePiece);            
                }
                else{
                boardHole.SpawnPiece();
                Clear(activePiece);
                SpawnPiece(); cnt = 1;
                }
            
            }      
    }

    public void SpawnPiece()
    {
        TetrominoData dataCopy = boardNext.pieceNexts[0];
        boardNext.pieceNexts.Remove(boardNext.pieceNexts[0]);
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];
        boardNext.pieceNexts.Add(data);  
        boardNext.Clear();
        boardNext.SpawnPiece();
        activePiece.Initialize(this, spawnPosition, dataCopy);
        if(IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else{
            GameOver();
        }
    }

    public void ClearHole(Piece piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);    
        }
    }
    
    public void SetHole(PieceHole piece)
    {
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);    
        }
    }
    
    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
        SceneManager.LoadScene(0);
    
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
    
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if(!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            
            if(this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }
    
    public bool IsValidPositionHole(PieceHole piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if(!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            
            if(this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }
    
    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while(row < bounds.yMax)
        {
            if(IsLineFull(row))
            {
                LineClear(row);
            }else{
                row++;
            }
        }


    }
    
    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for(int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if(!this.tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }
    
    private void LineClear(int row)
    {
        diem+=100;
        _diem.UpdateDiem();
        RectInt bounds = this.Bounds;
        for(int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while(row < bounds.yMax)
        {
            for(int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row+1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }
            row++;  
        }
    }
}
