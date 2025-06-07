using UnityEngine;

public class GlobalHandler : MonoBehaviour
{
    public static GlobalHandler Instance { get; private set; }
    public SceneHandler sceneHandler;

    public AnimationHandler animationHandler;

    public Camera mainCamera;

    [HideInInspector]
    public float deltaTime;

    private int frameRate = 60; // Default frame rate

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
        sceneHandler = new SceneHandler();
        animationHandler = new AnimationHandler();
        mainCamera = Camera.main; // Get the main camera
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
    }

    void UpdateFrame()
    {
        sceneHandler.UpdateFrame();
        animationHandler.UpdateFrame();
    }

    public void OnGameOver()
    {
        
    }

}
