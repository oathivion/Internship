using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public Text scoreText;

    private float timeSinceLastKill = 0f;

    void Awake()
    {
        if (Instance == null)
        {
           Instance = this;
          DontDestroyOnLoad(gameObject);  // <- This keeps it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates if scene is reloaded
        }
    }

    void Update()
    {
        timeSinceLastKill += Time.deltaTime;
    }

    public void EnemyDefeated()
    {
        int timeBonus = Mathf.Max(0, 100 - Mathf.RoundToInt((timeSinceLastKill / 3f) * 100));
        int earned = 100 + timeBonus;

        score += earned;
        timeSinceLastKill = 0f;

        Debug.Log($"Enemy defeated! +{earned} points (Time Bonus: {timeBonus})");

        UpdateUI();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    } 
}
