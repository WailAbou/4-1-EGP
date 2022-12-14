using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public List<Vector2Int> StartCoordinates = new List<Vector2Int>();
    public CellType[] CellTypes = new CellType[10 * 10];
}