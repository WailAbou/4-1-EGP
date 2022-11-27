using UnityEngine;
using DG.Tweening;

public class CellAnimation : MonoBehaviour, ICellAnimation
{
    private Renderer _renderer;
    private Color _startColor;
    private float _startHeight;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnAnimation()
    {
        _startColor = _renderer.material.color;
        _startHeight = transform.position.y;
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Animations.GRIDPIECE_SPAWN_DURATION));
    }

    public void HoverEnterAnimation(CellLogic cell)
    {
        _renderer.material.color = Color.white;
    }

    public void HoverLeaveAnimation(CellLogic cell)
    {
        _renderer.material.color = _startColor;
    }
}
