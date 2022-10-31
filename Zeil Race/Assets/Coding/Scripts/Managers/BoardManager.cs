using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardManager : Singleton<BoardManager>
{
    [Header("BoardManager References")]
    public BoardAnimation BoardAnimation;
    public GridPiece[,] GridPieces;
    public GridPiece SelectedPiece;

    private GridPiece _hoverPiece;

    public GridPiece FindPiece(GameObject go) => GridPieces.Cast<GridPiece>().Where(gp => gp.go == go).FirstOrDefault();

    public void HoverPiece(GridPiece piece)
    {
        if (_hoverPiece != null && _hoverPiece != piece)
            _hoverPiece.Anim.OnHoverLeaveAnimation();

        _hoverPiece = piece;
        _hoverPiece.Anim.OnHoverEnterAnimation();
    }

    public void SelectPiece(GridPiece piece)
    {
        SelectedPiece = piece;
        Debug.Log(SelectedPiece.go.transform.position);
    }
}
