using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Settings")]
    public bool dontDestroyOnLoad;
    public bool destroyDuplicate;

    public static T Instance => instance;
    private static T instance;

    protected CameraManager _cameraManager;
    protected GeneratorManager _generatorManager;
    protected BoardManager _boardManager;
    protected PlayerManager _playerManager;
    protected UiManager _uiManager;
    protected QuizManager _quizManager;

    public virtual void Awake()
    {
        instance = gameObject.GetComponent<T>();
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public virtual void Start()
    {
        if (destroyDuplicate && Instance != this) Destroy(gameObject);

        _cameraManager = CameraManager.Instance;
        _generatorManager = GeneratorManager.Instance;
        _boardManager = BoardManager.Instance;
        _playerManager = PlayerManager.Instance;
        _uiManager = UiManager.Instance;
        _quizManager = QuizManager.Instance;

        Setup();
    }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (destroyDuplicate && Instance != this) Destroy(gameObject);
    }

    public virtual void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    public virtual void Setup() { }
}