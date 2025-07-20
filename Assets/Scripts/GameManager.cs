using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 0;
    public int enemiesRemaining = 0;
    public int baseEnemiesPerLevel = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Listen for scene loads
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Easy_Level") // ← Replace with your actual level scene name
        {
            StartLevel();
        }
    }
    public void StartLevel()
    {
        currentLevel++;
        enemiesRemaining = baseEnemiesPerLevel + currentLevel;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        SpawnZone zone = FindObjectOfType<SpawnZone>(); // or FindObjectsOfType if using multiple zones
        if (zone != null)
        {
            zone.SpawnEnemies(enemiesRemaining);
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            Debug.Log("Level Complete!");
            LoadLevelPath();
            Invoke(nameof(StartLevel), 2f); // Wait before starting next level
        }
    }
    void LoadLevelPath()
    {
        SceneManager.LoadScene("Assets/Scenes/Medium_Level.unity"); // OR use the scene index
    }
}

