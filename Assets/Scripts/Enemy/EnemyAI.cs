using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : AIMovement, IHealth
{
    public AIType myType;

    #region Variables

    [SerializeField] private float health;
    [SerializeField] private float currhealth;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float deathAnimTimer = 2f;
    [SerializeField] private int points = 20;
    public Transform headPos;
    #endregion

    #region Refrences

    private Rigidbody rb;
    private CapsuleCollider cCollider;

    #endregion

    private void Awake()
    {
        //Initializing References.
        rb = GetComponent<Rigidbody>();
        cCollider = rb.GetComponent<CapsuleCollider>();
        nMAgent = rb.GetComponent<NavMeshAgent>();
        animator = rb.GetComponent<Animator>();
        nMObstacle = rb.GetComponent<NavMeshObstacle>();
        currhealth = health;
        //Setting Auto Braking to false so the Agent do not slow down upon reaching a point.
        nMAgent.autoBraking = false;
        nMAgent.speed = speed;
    }

    public float GetCurrentHealth => currhealth;

    //For taking Damage and Reduce health
    public void GetDamage(float damage)
    {
        if (currhealth > 0)
        {
            currhealth -= damage;
            if (currhealth <= 0)
            {
                animator?.SetTrigger("IsDead");
                EventManager.SetScore(points);
                EventManager.EnemyDeath(this);
                DisableNavigation();
            }
        }
    }

    //Reinitializing AI after Death
    public void ResetAI()
    {
        rb.useGravity = true;
        cCollider.enabled = true;
        nMObstacle.enabled = true;
        nMAgent.speed = speed;
        destPoint = 0;
        currhealth = health;
        reachedToEnd = false;
        gameObject.SetActive(false);
    }

    //For Damaging Main Building
    void DamageCastle()
    {
        EventManager.ApplyDamage(damage);
    }

    public void DisableNavigation()
    {
        rb.useGravity = false;
        cCollider.enabled = false;
        nMObstacle.enabled = false;
        nMAgent.speed = 0;
        Invoke(nameof(ResetAI), deathAnimTimer);
    }
}

interface IHealth
{
    public void GetDamage(float damage);
}