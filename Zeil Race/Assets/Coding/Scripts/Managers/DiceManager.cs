using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : Singleton<DiceManager>
{
    public Action OnStartDiceRolls;
    public Action<int> OnDiceRolled;
    public Action<Vector2Int> OnEndDiceRolls;

    private int[] _diceRolls = new int[2];
    private int _diceIndex;
    public GameObject diceObject;
    private bool isThrowing;
    public override void Setup()
    {
        _playerManager.OnTurnStart += StartDiceRolls;
        isThrowing = false;
    }

    private void Update()
    {
        if (diceObject.GetComponent<Rigidbody>().velocity == Vector3.zero && isThrowing)
        {
            StartCoroutine(GetDiceRoll());
        }
    }

    public void StartDiceRolls(Transform player, Vector2Int gridPosition)
    {
       _diceIndex = 0;
        OnStartDiceRolls?.Invoke();
    }

    /// <summary>
    /// Stores the rolled dice number and goes on to the next one or ends the rolls if it is the last one.
    /// </summary>
    /// <param name="diceRoll">The rolled number of the current dice.</param>
    public void EndRollDices(int diceRoll)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-10, 10) / 10f, Random.Range(2, 7) / 10f, Random.Range(-10, 10) / 10f);
        Vector3 spawnAngle = new Vector3(Random.Range(0, 360), Random.Range(0, 50), Random.Range(0, 360));
        Vector3 randomForce = new Vector3(Random.Range(20, 40), Random.Range(20, 40), Random.Range(20, 40));
        Debug.Log(spawnPos);
        diceObject.transform.position = spawnPos;
        diceObject.GetComponent<Rigidbody>().AddTorque(spawnAngle);
        diceObject.GetComponent<Rigidbody>().AddForce(randomForce);
        
        StartCoroutine(StartThrow());
        

        _diceRolls[_diceIndex] = diceRoll;
        _diceIndex++;

        if (_diceIndex < 2) OnDiceRolled?.Invoke(diceRoll);
        else OnEndDiceRolls?.Invoke(new Vector2Int(_diceRolls[0], _diceRolls[1]));
    }

    IEnumerator StartThrow()
    {
        yield return new WaitForSeconds(1);
        isThrowing = true;
    }

    IEnumerator GetDiceRoll()
    {
        if (Vector3.Angle(diceObject.transform.forward, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 1;
        if (Vector3.Angle(-diceObject.transform.forward, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 6;
        if (Vector3.Angle(diceObject.transform.up, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 5;
        if (Vector3.Angle(-diceObject.transform.up, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 2;
        if (Vector3.Angle(diceObject.transform.right, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 4;
        if (Vector3.Angle(-diceObject.transform.right, Vector3.up) < 0.6f)
            _diceRolls[_diceIndex] = 3;
        Debug.Log(_diceRolls[_diceIndex]);
        isThrowing = false;
        yield return null;
    }
}
