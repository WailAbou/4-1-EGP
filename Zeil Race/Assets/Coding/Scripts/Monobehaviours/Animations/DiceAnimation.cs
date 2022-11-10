using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class DiceAnimation : MonoBehaviour, IDiceAnimation
{
    private List<RectTransform> _dicePanels = new List<RectTransform>();
    private List<TMP_Text> _diceDisplays = new List<TMP_Text>();
    private List<Sequence> _diceSequences = new List<Sequence>();
    private int _counter = -1;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _dicePanels.Add(child.GetComponent<RectTransform>());
            _diceDisplays.Add(child.GetComponentInChildren<TMP_Text>());
        }
    }

    public void MoveStartAnimation()
    {
        for (int i = 0; i < 2; i++)
        {
            var sequence = DOTween.Sequence();
            var dicePanel = _dicePanels[i];
            var diceDisplay = _diceDisplays[i];

            sequence.Append(dicePanel.DOAnchorPosY(50, Constants.DICE_SPAWN_DURATION));
            sequence.Join(DOTween.To(() => _counter, x => SetDiceDisplay(diceDisplay, x), 5, Constants.DICE_MOVE_DURATION).SetLoops(int.MaxValue, LoopType.Restart));

            _diceSequences.Add(sequence);
        }
    }

    private void SetDiceDisplay(TMP_Text diceDisplay, int x)
    {
        _counter = x + 1;
        diceDisplay.SetText(_counter.ToString());
    }

    public int MoveStopAnimation(int diceIndex)
    {
        _diceSequences[diceIndex].Kill();
        return _counter;
    }

    public void MoveEndAnimation(GridCell gridCell)
    {
        _dicePanels.ForEach(dicePanel => dicePanel.DOAnchorPosY(-150, Constants.DICE_END_DURATION));
        _diceSequences.Clear();
        _counter = 0;
    }
}
