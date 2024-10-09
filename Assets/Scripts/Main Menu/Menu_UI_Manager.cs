using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_UI_Manager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    [SerializeField] TextMeshProUGUI playerScore;

    private void OnEnable()
    {
        //Initializing Menu Scene
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        playerScore.SetText("SCORE: "+PlayerPrefs.GetInt("Player_Score").ToString(), 00);
    }

    //For Entering Game Level
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    //On Pressing Quit Button
    public void QuitGame()
    {
        Application.Quit();
    }
}