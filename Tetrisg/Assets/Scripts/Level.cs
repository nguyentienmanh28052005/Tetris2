using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Board board;

    public TextMeshProUGUI level;


    public void UpdateLevel()
    {
        level.text = "Level : " + board.level.ToString();
    }
    
}
