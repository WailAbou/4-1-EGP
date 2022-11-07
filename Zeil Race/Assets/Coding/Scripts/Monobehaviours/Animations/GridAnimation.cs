using UnityEngine;
using DG.Tweening;

public class GridAnimation : MonoBehaviour
{
    private float _startHeight;
    private Renderer _renderer;
    private Color _startColor;
    private BoardManager _boardManager;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _startHeight = transform.position.y;
        _startColor = _renderer.material.color;
        _boardManager = BoardManager.Instance;

        _boardManager.OnHoverEnter += HoverEnterAnimation;
        _boardManager.OnHoverLeave += HoverLeaveAnimation;
        StartAnimation();
    }

    private void StartAnimation()
    {
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Constants.GRIDPIECE_START_DURATION));
    }

    public void HoverEnterAnimation(GridPiece gridPiece)
    {
        if (gridPiece.Anim == this) 
            _renderer.material.color = Color.white;
    }

    public void HoverLeaveAnimation(GridPiece gridPiece)
    {
        if (gridPiece.Anim == this)
            _renderer.material.color = _startColor;
    }
}
