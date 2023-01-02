using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Linq;
using UnityEngine;

public class BoardAnimation : MonoBehaviour, IBoardAnimation
{
    [Header("BoardAnimation References")]
    public Transform LinesHolder;
    public GameObject LinePrefab;

    public void SpawnAnimation(CellLogic startCell, CellLogic endCell, Vector2Int gridSize)
    {
        var startPos = startCell.transform.position;
        var endPos = endCell.transform.position;
        var gridDimensions = new Vector2(0.3f, 0.3f);

        var xTweens = SetupAxis(gridSize.x, new Vector3(0, 0, gridDimensions.y), new Vector3(endPos.x, startPos.y + 0.02f, startPos.z));
        var yTweens = SetupAxis(gridSize.y, new Vector3(gridDimensions.x, 0, 0), new Vector3(startPos.x, startPos.y + 0.02f, endPos.z));
        SetupAnimation(xTweens, yTweens);
    }

    private void SetupAnimation(TweenerCore<Vector3, Vector3, VectorOptions>[] xTweens, TweenerCore<Vector3, Vector3, VectorOptions>[] yTweens)
    {
        var sequence = DOTween.Sequence();
        float quarter = Animations.BOARD_SPAWN_DURATION / 4;

        var boardTween = transform.DOPunchScale(new Vector3(0.25f, 0, 0.25f), quarter);
        sequence.Append(boardTween).AppendInterval(quarter / 3);

        sequence.Join(xTweens[0]).Join(yTweens[0]);
        for (int i = 1; i < Mathf.Max(xTweens.Length, yTweens.Length); i++) { sequence.Append(xTweens[i]).Join(yTweens[i]); }
    }

    private TweenerCore<Vector3, Vector3, VectorOptions>[] SetupAxis(int amount, Vector3 offset, Vector3 targetPosition)
    {
        float startDuration = (Animations.BOARD_SPAWN_DURATION / 2) / 3;
        float normalDuration = (Animations.BOARD_SPAWN_DURATION / 2) / (amount - 1);

        var tweens = new TweenerCore<Vector3, Vector3, VectorOptions>[amount];
        for (int i = 0; i < amount; i++)
        {
            var line = Instantiate(LinePrefab, LinesHolder).GetComponent<LineRenderer>();
            if (i == 0) line.widthMultiplier = 0.02f;

            line.SetPosition(0, line.GetPosition(0) + offset * i);
            line.SetPosition(1, line.GetPosition(1) + offset * i);

            tweens[i] = DOTween.To(() => line.GetPosition(1), x => line.SetPosition(1, x), targetPosition + offset * i, (i == 0) ? startDuration : normalDuration);
        }
        return tweens;
    }
}
