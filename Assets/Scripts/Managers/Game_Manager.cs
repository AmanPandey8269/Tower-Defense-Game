using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] int gameScore = 0;
    [SerializeField] int enemyCount;
    [SerializeField] int current_Wave = 0;
    [SerializeField] int total_Waves;
    [SerializeField] int autoScoreUpdateTimer;
    [SerializeField] int autoGenerateScore = 5;
    [SerializeField] float castle_Health;
    [SerializeField] float coolDown_Timer = 2f;
    [SerializeField] List<WaveInfo> Waves;

    public int GetScore
    {
        get => gameScore;
        set { gameScore = value; }
    }

    public UI_Manager uiManager;
    public Turret_Manager turretManager;

    private void OnEnable()
    {
        //Initializing Manager
        EventManager.SubscribetoScore(EnemyKilled);
        EventManager.SubscribetoDamage(UpdateCastleHealth);
        total_Waves = Waves.Count;
        uiManager.SetUpTurrets(turretManager.turrets);
    }
    public  void Start()
    {
        //Starting first Enemy Wave
        StartCoroutine(SetupWave());
        Invoke(nameof(AutoGenerateScore), autoScoreUpdateTimer);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribetoScore(EnemyKilled);
        EventManager.UnSubscribetoDamage(UpdateCastleHealth);
    }


    //For Generating Score Automatically
    void AutoGenerateScore()
    {
        SetScore(autoGenerateScore);
        Invoke(nameof(AutoGenerateScore), autoScoreUpdateTimer);
    }

    //Setting Up New Wave Data
    IEnumerator SetupWave()
    {
        yield return new WaitForSeconds(coolDown_Timer);
        if (current_Wave < total_Waves)
        {
            uiManager.GiveWaveWarning(Waves[current_Wave].waveName);
            for (int i = 0; i < Waves[current_Wave].randomAICount; i++)
            {
                int type = Random.Range(0, (int)Waves[current_Wave].difficulty - 1);
                EventManager.spawnAI((AIType)type);
                enemyCount++;
                yield return new WaitForSeconds(0.5f);
            }
            for (int i = 0; i < Waves[current_Wave].aICount; i++)
            {
                EventManager.spawnAI(Waves[current_Wave].difficulty);
                enemyCount++;
                yield return new WaitForSeconds(0.5f);
            }
            current_Wave++;
            uiManager.SetWaveCount(current_Wave);
        }
        else
        {
            uiManager.OpenWinPanel();
            PlayerPrefs.SetInt("Player_Score", GetScore);
        }
    }

    //Updating Main Building Health
    void UpdateCastleHealth(float damage)
    {
        castle_Health -= damage;
        uiManager.SetCastleHealth(castle_Health);
    }

    //Setting Score and Enemy Count
    void EnemyKilled(int killPoints)
    {
        enemyCount -= 1;
        if (enemyCount == 0)
        {
            StartCoroutine(SetupWave());
        }
        SetScore(killPoints);
    }


    //Setting Score
    void SetScore(int killPoints)
    {
        GetScore += killPoints;
        uiManager.SetScore(GetScore);
    }
}

[System.Serializable]
public class WaveInfo
{
    public int wave;
    public string waveName;
    public int aICount;
    public int randomAICount;
    public AIType difficulty;
}