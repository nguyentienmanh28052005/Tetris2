using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Timeline;

public class Piece : MonoBehaviour
{
    public Board board { get; private set;}
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }
    [SerializeField] PieceHole pieceHole;

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float moveDelay = 0.2f;
    private float stepTime;
    private float lockTime;
    private float moveTime;
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;

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
        this.board.Clear(this);
        this.lockTime += Time.deltaTime;
        //Debug.Log(lockTime);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }else if(Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }

        if(Input.GetKey(KeyCode.A))
        {
            if(Time.time >= this.moveTime)
            {
                move(2);
            }
        }
        if(Input.GetKey(KeyCode.D))
        {
            if(Time.time >= this.moveTime)
            {
                move(1);
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            if(Time.time >= this.moveTime)
            {
                move(3);
            }
        } 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HandDrop();
        }
        if(Time.time >= this.stepTime)
        {
            Step();
        }
        this.board.Set(this);
        if(Input.GetKeyDown(KeyCode.X))
        {
            this.board.ClearHole(this);
            this.board.SetHole(pieceHole);
        }
    }

    private void move(int x)
    {
        this.moveTime = Time.time + this.moveDelay;
        if(x==1) Move(Vector2Int.right);
        else if(x==2) Move(Vector2Int.left);
        else if(x==3) Move(Vector2Int.down);
    }
    
    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;
        Move(Vector2Int.down);
       // Debug.Log(this.lockTime);
        if(this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }


    private void Lock()
    {
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
    }

    private void HandDrop()
    {
        while(Move(Vector2Int.down)) continue;
        Lock();
    }

    private bool Move(Vector2Int transaltion)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += transaltion.x;
        newPosition.y += transaltion.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if(valid)
        {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction)
    {
        int originalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0, 4);

        ApplyRotationMatix(direction);

        if(!TestWallKicks(this.rotationIndex, direction))
        {
            this.rotationIndex = originalRotation;
            ApplyRotationMatix(-direction);
        }
    }

    private void ApplyRotationMatix(int direction)
    {
        for(int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];

            int x, y;
            switch( this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                cell.x -= 0.5f;
                cell.y -= 0.5f;
                x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                break;

                default:
                x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {   
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for(int i = 0; i < this.data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

            if(Move(translation))
            {
                return true;
            }
        }

        return false;   
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if(rotationDirection < 0)
        {
            wallKickIndex--;
        }
        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }
    private int Wrap(int input, int min, int max)
    {
        if (input < min){
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }

    internal void Initialize(BoardHole boardHole, Vector3Int spawnPosition, TetrominoData data)
    {
        throw new NotImplementedException();
    }
}
