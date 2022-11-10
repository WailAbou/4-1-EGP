using UnityEngine;

public abstract class BaseLogic<T> : MonoBehaviour
{
    protected CameraManager _cameraManager;
    protected GeneratorManager _generatorManager;
    protected GridManager _gridManager;
    protected PlayerManager _playerManager;
    protected UiManager _uiManager;
    protected QuizManager _quizManager;
    protected DiceManager _diceManager;
    protected GameManager _gameManager;
    protected T _animation;

    protected virtual void Start()
    {
        _cameraManager = CameraManager.Instance;
        _generatorManager = GeneratorManager.Instance;
        _gridManager = GridManager.Instance;
        _playerManager = PlayerManager.Instance;
        _uiManager = UiManager.Instance;
        _quizManager = QuizManager.Instance;
        _diceManager = DiceManager.Instance;
        _gameManager = GameManager.Instance;
        _animation = GetComponent<T>();

        SetupLogic();
        SetupAnimation();
    }

    protected virtual void SetupLogic() { }

    protected virtual void SetupAnimation() { }
}
