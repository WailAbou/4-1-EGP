using DG.Tweening;
using UnityEngine;

public class BoardAnimation : MonoBehaviour
{
    private void Start()
    {
        GeneratorManager.Instance.OnGenerateDone += StartAnimation;
    }

    public void StartAnimation()
    {
        transform.DOPunchScale(new Vector3(0.25f, 0, 0.25f), Constants.BOARD_START_DURATION);
    }
}
