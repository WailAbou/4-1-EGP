using UnityEngine;
using DG.Tweening;
using TMPro;

public class CellAnimation : MonoBehaviour, ICellAnimation
{
    [Header("CellAnimation References")]
    public GameObject CoordinatesPrefab;

    private Renderer _renderer;
    private Color _startColor;
    private float _startHeight;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SpawnAnimation(Vector2Int coordinates, Transform boardCoordinatesHolder)
    {
        _startColor = _renderer.material.color;
        _startHeight = transform.position.y;
        transform.DOMoveY(_startHeight + 1.0f, 0).OnComplete(() => transform.DOMoveY(_startHeight, Animations.GRIDPIECE_SPAWN_DURATION));

        if (coordinates.x == 0 || coordinates.y == 0)
        {
            Vector3 offset = (coordinates.y == 0) ? Vector3.forward * -0.2f : Vector3.right * -0.2f;
            offset = coordinates == Vector2Int.zero ? (Vector3.forward + Vector3.right) * -0.2f : offset;
            offset += Vector3.up * 0.1f;

            var display = (coordinates.y == 0) ? coordinates.x.ToString() : coordinates.y.ToString();
            var coordinatesGo = Instantiate(CoordinatesPrefab, transform.position + offset, Quaternion.identity, boardCoordinatesHolder);
            
            coordinatesGo.GetComponentInChildren<TMP_Text>().SetText(display);
            coordinatesGo.transform.DOScale(0.2f, 0.5f);
        }
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
