using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform AI_Spawner;

    [SerializeField] List<EnemyAI> enemyAIs;
    [SerializeField] List<EnemyAI> aIPool;

    private void OnEnable()
    {
        EventManager.SubscribetoAISpawner(SpawnAI);
        EventManager.SubscribetoEnemyDeath(AddAItoPool);
    }
    private void OnDisable()
    {
        EventManager.UnSubscribetoAISpawner(SpawnAI);
        EventManager.UnSubscribetoEnemyDeath(AddAItoPool);
    }

    void AddAItoPool(EnemyAI aI)
    {
        aIPool.Add(aI);
    }

    void SpawnAI(AIType type)
    {
        for (int i = 0; i < aIPool.Count; i++)
        {
            if (aIPool[i].myType.Equals(type))
            {
                aIPool[i].transform.position = AI_Spawner.transform.position;
                aIPool[i].gameObject.SetActive(true);
                aIPool[i].pathWay = wayPoints;
                aIPool[i].MovetoNextPoint();
                aIPool.RemoveAt(i);
                return;
            }
        }
        for (int i = 0; i < enemyAIs.Count; i++)
        {
            if (enemyAIs[i].myType.Equals(type))
            {
                EnemyAI AI = Instantiate(enemyAIs[i], AI_Spawner.transform.position, AI_Spawner.transform.rotation);
                AI.pathWay = wayPoints;
                AI.MovetoNextPoint();
                return;
            }
        }
    }

    public Transform GetNextWayPoint(int waypointPos)
    {
        if (wayPoints[waypointPos] != null)
            return wayPoints[waypointPos];
        else return null;
    }
}

public enum AIType
{
    Easy = 0,
    Medium = 1,
    Hard = 2,
    VeryHard = 3
}