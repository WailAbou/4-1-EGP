using UnityEngine;

public class GridMechanic : MonoBehaviour
{
    [HideInInspector]
    public GridPiece GridPiece;
    public bool HasQuestion;

    private PlayerManager _playerManager;
    private GameManager _gameManager;
    private BoardManager _boardManager;
    private bool _gameStarted;
    private bool _questionAsked;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _gameManager = GameManager.Instance;
        _boardManager = BoardManager.Instance;

        _playerManager.OnPlayersSpawned += _ => _gameStarted = true;
        _gameManager.OnQuizCorrect += OnQuizCorrect;
    }

    private void OnMouseEnter()
    {
        if (_gameStarted) _boardManager.HoverPiece(GridPiece);
    }

    private void OnMouseDown()
    {
        if (_gameStarted && HasQuestion)
        {
            _gameManager.StartQuiz();
            _questionAsked = true;
        }
    }

    private void OnQuizCorrect(QuizScriptableObject quiz)
    {
        if (!_questionAsked) return;

        _boardManager.SelectPiece(GridPiece);
    }
}
