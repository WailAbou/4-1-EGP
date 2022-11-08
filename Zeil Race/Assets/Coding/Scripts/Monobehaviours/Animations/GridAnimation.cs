using UnityEngine;
using DG.Tweening;

public class GridAnimation : MonoBehaviour, IGridAnimation
{
    private Renderer _renderer;
    private Color _startColor;
    private float _startHeight;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
        _startHeight = transform.position.y;
    }

    public void SpawnAnimation()
    {
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Constants.GRIDPIECE_SPAWN_DURATION));
    }

    public void HoverEnterAnimation(GridCell gridCell)
    {
        _renderer.material.color = Color.white;
    }

    public void HoverLeaveAnimation(GridCell gridCell)
    {
        _renderer.material.color = _startColor;
    }
}
