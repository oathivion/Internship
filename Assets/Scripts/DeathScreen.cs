using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;
    public Text scoreText;
    public Text levelText;

    public void Show(int finalScore, int levelsBeaten)
    {
        deathScreenUI.SetActive(true);
        scoreText.text = "Score: " + finalScore;
        levelText.text = "Levels Beaten: " + levelsBeaten;
    }

    public void RestartGame()
    {
        ScoreManager.Instance?.ResetScore();
        GameManager.Instance?.ResetProgress();
        SceneManager.LoadScene("Tutoral");
    }
}
