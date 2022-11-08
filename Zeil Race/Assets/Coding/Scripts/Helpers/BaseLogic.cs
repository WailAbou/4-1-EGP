using UnityEngine;

public abstract class BaseLogic<T> : MonoBehaviour
{
    protected CameraManager _cameraManager;
    protected GeneratorManager _generatorManager;
    protected BoardManager _boardManager;
    protected PlayerManager _playerManager;
    protected UiManager _uiManager;
    protected QuizManager _quizManager;
    protected T _animation;

    protected virtual void Start()
    {
        _cameraManager = CameraManager.Instance;
        _generatorManager = GeneratorManager.Instance;
        _boardManager = BoardManager.Instance;
        _playerManager = PlayerManager.Instance;
        _uiManager = UiManager.Instance;
        _quizManager = QuizManager.Instance;
        _animation = GetComponent<T>();

        SetupLogic();
        SetupAnimation();
    }

    protected virtual void SetupLogic() { }

    protected virtual void SetupAnimation() { }
}
