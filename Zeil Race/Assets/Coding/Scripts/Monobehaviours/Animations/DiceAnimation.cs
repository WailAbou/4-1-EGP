using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using Random = UnityEngine.Random;

public class DiceAnimation : MonoBehaviour, IDiceAnimation
{
    public Rigidbody DiceRigidBody;

    private List<RectTransform> _dicePanels = new List<RectTransform>();
    private List<TMP_Text> _diceDisplays = new List<TMP_Text> ();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _dicePanels.Add(child.GetComponent<RectTransform>());
            _diceDisplays.Add(child.GetComponentInChildren<TMP_Text>());
        }
    }

    public void MoveStartAnimation(int allowedRolls)
    {
        DiceRigidBody.position = new Vector3(0, 1, 0);
        DiceRigidBody.transform.DOLocalRotate(new Vector3(360, 0, 360), Animations.DICE_MOVE_DURATION, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(int.MaxValue, LoopType.Restart);
    }

    private int GetDiceRoll()
    {
        int rolled = 1;
        var dice = DiceRigidBody.transform;
        Vector3[] directions = new Vector3[6] { dice.forward, -dice.up, -dice.right, dice.right, dice.up, -dice.forward };
        for (int i = 0; i < directions.Length; i++) { 
            if (Vector3.Angle(directions[i], Vector3.up) < 0.6f) rolled = i + 1; 
        }
        return rolled; 
    }

    private IEnumerator ThrowDice(int diceIndex, Action<int> onDiceStop)
    {
        Vector3 spawnAngle = new Vector3(Random.Range(0, 360), Random.Range(0, 50), Random.Range(0, 360));
        Vector3 randomForce = new Vector3(Random.Range(20, 40), Random.Range(20, 40), Random.Range(20, 40));

        DiceRigidBody.useGravity = true;
        DiceRigidBody.AddTorque(spawnAngle);
        DiceRigidBody.AddForce(randomForce);

        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => DiceRigidBody.velocity == Vector3.zero);

        var diceRoll = GetDiceRoll();
        _diceDisplays[diceIndex].SetText(diceRoll.ToString());
        _dicePanels[diceIndex].DOAnchorPosY(50, Animations.DICE_SPAWN_DURATION);
        onDiceStop(diceRoll);
    }

    public void MoveStopAnimation(int diceIndex, Action<int> onDiceStop)
    {
        DiceRigidBody.transform.DOKill();
        StartCoroutine(ThrowDice(diceIndex, onDiceStop));
    }

    public void MoveEndAnimation(CellLogic cell, int allowedRolls)
    {
        DiceRigidBody.useGravity = false;
        DiceRigidBody.position = new Vector3(0, 10, 0);

        for (int i = 0; i < allowedRolls; i++)
        {
            _dicePanels[i].DOAnchorPosY(-150, Animations.DICE_END_DURATION);
        }
    }
}
