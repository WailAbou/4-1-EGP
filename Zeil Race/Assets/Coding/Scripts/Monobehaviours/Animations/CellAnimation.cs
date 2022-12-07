using UnityEngine;
using DG.Tweening;
using TMPro;

public class CellAnimation : MonoBehaviour, ICellAnimation
{
    [Header("CellAnimation References")]
    public GameObject CoordinatesPrefab;

    private GameObject _coordinatesGo;
    private Renderer _renderer;
    private Color _startColor;
    private float _startHeight;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnAnimation(Vector2Int coordinates)
    {
        _startColor = _renderer.material.color;
        _startHeight = transform.position.y;
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Animations.GRIDPIECE_SPAWN_DURATION));

        _coordinatesGo = Instantiate(CoordinatesPrefab, transform);
        _coordinatesGo.GetComponentInChildren<TMP_Text>().SetText(coordinates.ToString());
    }

    public void HoverEnterAnimation(CellLogic cell)
    {
        _renderer.material.color = Color.white;
    }

    public void HoverLeaveAnimation(CellLogic cell)
    {
        _renderer.material.color = _startColor;
    }

    public void DisplayCoordinates(int camIndex)
    {
        bool display = (camIndex == 1);
        _coordinatesGo.SetActive(display);
    }
}
