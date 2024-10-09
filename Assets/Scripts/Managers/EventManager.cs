using System;

public static class EventManager
{
    public static Action<int> SetScore;
    public static Action<EnemyAI> EnemyDeath;
    public static Action<float> ApplyDamage;
    public static Action<AIType> spawnAI;

    public static void SubscribetoScore(Action<int> Subscriber)
    {
        SetScore += Subscriber;
    }

    public static void UnSubscribetoScore(Action<int> Subscriber)
    {
        SetScore -= Subscriber;
    }
    
    public static void SubscribetoEnemyDeath(Action<EnemyAI> Subscriber)
    {
        EnemyDeath += Subscriber;
    }

    public static void UnSubscribetoEnemyDeath(Action<EnemyAI> Subscriber)
    {
        EnemyDeath -= Subscriber;
    }
    public static void SubscribetoAISpawner(Action<AIType> Subscriber)
    {
        spawnAI += Subscriber;
    }

    public static void UnSubscribetoAISpawner(Action<AIType> Subscriber)
    {
        spawnAI -= Subscriber;
    }
    public static void SubscribetoDamage(Action<float> Subscriber)
    {
        ApplyDamage += Subscriber;
    }

    public static void UnSubscribetoDamage(Action<float> Subscriber)
    {
        ApplyDamage -= Subscriber;
    }
}