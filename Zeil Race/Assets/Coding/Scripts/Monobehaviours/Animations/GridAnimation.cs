using UnityEngine;
using DG.Tweening;

public class GridAnimation : MonoBehaviour
{
    private float _startHeight;
    private Renderer _renderer;
    private Color _startColor;


    private void Awake()
    {
        _startHeight = transform.position.y;
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
    }

    private void Start() => OnStartAnimation();

    private void OnStartAnimation()
    {
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Constants.GRIDPIECE_START_DURATION));
    }

    public void OnHoverEnterAnimation()
    {
        _renderer.material.color = Color.white;
    }

    public void OnHoverLeaveAnimation()
    {
        _renderer.material.color = _startColor;
    }
}
