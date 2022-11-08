using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    public Renderer Sail;

    private PlayerManager _playerManager;
    private BoardManager _boardManager;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _boardManager = BoardManager.Instance;

        _boardManager.OnSelect += TakeTurn;
    }

    private void TakeTurn(GridPiece gridPiece)
    {
        if (!_playerManager.CheckTurn(this)) return;

        _playerManager.TakeTurn(transform, gridPiece.gameObject.transform);
    }
}
