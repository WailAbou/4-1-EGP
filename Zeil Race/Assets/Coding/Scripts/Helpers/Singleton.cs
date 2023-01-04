using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Settings")]
    public bool OnLoadDontDestroy;
    public bool DuplicatesDestroy;

    public static T Instance => _instance;
    private static T _instance;

    protected CameraManager _cameraManager;
    protected GeneratorManager _generatorManager;
    protected CellManager _cellManager;
    protected PlayerManager _playerManager;
    protected UiManager _uiManager;
    protected QuizManager _quizManager;
    protected DiceManager _diceManager;
    protected GameManager _gameManager;
    protected RewardManager _rewardManager;

    public virtual void Awake()
    {
        _instance = gameObject.GetComponent<T>();
        if (OnLoadDontDestroy) DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public virtual void Start()
    {
        if (DuplicatesDestroy && Instance != this) Destroy(gameObject);

        _cameraManager = CameraManager.Instance;
        _generatorManager = GeneratorManager.Instance;
        _cellManager = CellManager.Instance;
        _playerManager = PlayerManager.Instance;
        _uiManager = UiManager.Instance;
        _quizManager = QuizManager.Instance;
        _diceManager = DiceManager.Instance;
        _gameManager = GameManager.Instance;
        _rewardManager = RewardManager.Instance;

        Setup();
    }
    public virtual void Setup() { }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (DuplicatesDestroy && Instance != this) Destroy(gameObject);
    }

    public virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}