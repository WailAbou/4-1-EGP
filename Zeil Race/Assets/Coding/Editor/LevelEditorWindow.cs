using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    private Level _level;
    private Vector2Int _levelSize = new Vector2Int(10, 10);
    private int _clickedCell = -1;
    private bool _loaded;

    private string[] _cellContents;
    private string[] _cellDisplays => _cellMappings.Keys.ToArray();
    private Dictionary<string, CellType> _cellMappings = new Dictionary<string, CellType>() { { "W", CellType.Water}, { "L", CellType.Land }, { "F", CellType.Final } };
    private Dictionary<string, Color> _cellColors = new Dictionary<string, Color>() { { "W", Color.blue }, { "L", Color.green }, { "F", Color.red } };

    [MenuItem("Window/Custom/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        _level = EditorGUILayout.ObjectField(_level, typeof(Level), true) as Level;

        if (_level != null)
        {
            LevelEditor();
            if (GUILayout.Button("SAVE")) SaveLevel();
        }
    }

    private void LevelEditor()
    {
        if (!_loaded) LoadLevel();
        RenderLevelEditor();
        if (_clickedCell != -1) UpdateCell();
    }

    private void RenderLevelEditor()
    {
        Extensions.FlexibleSpace(10);
        for (int y = 0; y < _levelSize.y; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < _levelSize.x; x++)
            {
                int i = x + (90 - (10 * y));
                GUI.backgroundColor = _cellColors[_cellContents[i]];
                var buttonClicked = GUILayout.Button(_cellContents[i].ToString());
                if (buttonClicked) _clickedCell = i;
            }
            GUILayout.EndHorizontal();
        }
        Extensions.FlexibleSpace(10);

        GUI.backgroundColor = Color.white;
    }

    private void LoadLevel()
    {
        _cellContents = new string[_levelSize.x * _levelSize.y];
        _cellContents.Fill(_cellDisplays[0]);

        for (int y = 0; y < _levelSize.y; y++)
        {
            for (int x = 0; x < _levelSize.x; x++)
            {
                int i = x + (10 * y);
                var cellType = _level.CellTypes[i];
                _cellContents[i] = _cellMappings.FirstOrDefault(x => x.Value == cellType).Key;
            }
        }
        _loaded = true;
    }

    private void SaveLevel()
    {
        for (int y = 0; y < _levelSize.y; y++)
        {
            for (int x = 0; x < _levelSize.x; x++)
            {
                int i = x + (10 * y);
                var cellContent = _cellContents[i];
                _level.CellTypes[i] = _cellMappings[cellContent];
            }
        }

        EditorUtility.SetDirty(_level);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void UpdateCell()
    {
        int newIndex = Array.FindIndex(_cellDisplays, c => c == _cellContents[_clickedCell]);
        newIndex = (newIndex + 1) % _cellMappings.Count;

        _cellContents[_clickedCell] = _cellDisplays[newIndex];
        _clickedCell = -1;
    }
}
