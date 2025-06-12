using UnityEngine;
using UnityEngine.UI;

public class GlobalHandler : MonoBehaviour
{
    public static GlobalHandler Instance { get; private set; }
    public SceneHandler sceneHandler;

    public AnimationHandler animationHandler;
    public MusicHandler musicHandler;

    public Camera mainCamera;

    public GameObject StartScreen;

    public Button startButton;
    public Button exitButton;

    [HideInInspector]
    public float deltaTime;

    public int frameRate = 120; // Default frame rate

    private float lastUpdatedFrame = 0f;
    private void Awake()
    {
        // Ensure only one instance of GlobalHandler exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicHandler = new MusicHandler();
        sceneHandler = new SceneHandler();
        animationHandler = new AnimationHandler();
        mainCamera = Camera.main; // Get the main camera

        // Link buttons to their respective methods
        startButton.onClick.AddListener(OnGameStart);
        exitButton.onClick.AddListener(OnGameExit);
    }

    void Update()
    {
        // Make it update frame consistently at the specified frame rate
        float currentDeltaTime = Time.time - lastUpdatedFrame;
        if (currentDeltaTime > 1f / frameRate)
        {
            deltaTime = currentDeltaTime; // Calculate delta time
            lastUpdatedFrame = Time.time;
            UpdateFrame();
        }

        sceneHandler.Update();
    }

    void UpdateFrame()
    {
        sceneHandler.UpdateFrame();
        animationHandler.UpdateFrame();
    }

    public void OnGameStart()
    {
        sceneHandler.LoadMusic1();
        StartScreen.SetActive(false); // Hide the start screen when the game starts
    }

    public void OnGameExit()
    {
        // Handle game exit logic
        Application.Quit(); // Quit the application
        // Optionally, you can add code to save game state or settings here
    }

    public void OnGamePause()
    {
        // Pause the game logic
        Time.timeScale = 0f; // Stop time
        //musicHandler.PauseAllTracks(); // Pause all music tracks
    }

    public void OnGameResume()
    {
        // Resume the game logic
        Time.timeScale = 1f; // Resume time
        //musicHandler.ResumeAllTracks(); // Resume all music tracks
    }

    public void OnGameOver()
    {

    }

}
