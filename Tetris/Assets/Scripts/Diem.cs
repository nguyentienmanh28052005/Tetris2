using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Diem : MonoBehaviour
{
    public Board board;
    public TextMeshProUGUI diem;

    

    public void UpdateDiem()
    {
        diem.text = board.diem.ToString();

    }
}
