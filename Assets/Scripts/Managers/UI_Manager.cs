using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header ("For Visual Update")]
    [Space(5)]

    [SerializeField] Slider castleHealthSlider;
    [SerializeField] TextMeshProUGUI gameScore;
    [SerializeField] TextMeshProUGUI currentWave;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI gameEndScore;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject loosePanel;
    [SerializeField] GameObject pausePanel;

    [SerializeField] UI_Turret turretPrefab;
    [SerializeField] Transform turretParent;

    public void SetUpTurrets(List<Turret> turretList)
    {
        for (int i = 0; i < turretList.Count; i++)
        {
            UI_Turret turret = Instantiate(turretPrefab, turretParent);
            turret.SetUpTurret(turretList[i]);
        }
    }

    public void SetCastleHealth(float health)
    {
        castleHealthSlider.value = health;
        if (health <= 0)
        {
            OpenGameOver();
        }
    }

    public void SetScore(int score)
    {
        gameScore.SetText($"SCORE: {score}");
    }
    
    public void SetWaveCount(int wave)
    {
        currentWave.SetText($"WAVE: {wave}");
    }

    public void GiveWaveWarning(string wave)
    {
        warningText.SetText($"{wave} WAVE");
        Invoke(nameof(DisableWarningWave), 2f);
    }

    void OpenGameOver()
    {
        loosePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
        gameEndScore.SetText(gameScore.text);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level 1");
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    void DisableWarningWave()
    {
        warningText.SetText("");
    }
}