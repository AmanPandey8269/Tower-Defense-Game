using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    #region Variables

    [HideInInspector] public int destPoint = 0;
    [SerializeField] private float remainingDistance = 0.25f;
    private bool reachedToEnd;

    #endregion

    #region Refrences

    [HideInInspector] public Transform[] pathWay;
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshObstacle nMObstacle;
    [HideInInspector] public NavMeshAgent nMAgent;

    #endregion

    private void Update()
    {
        if (nMAgent.enabled)
        {
            if (nMAgent.remainingDistance < remainingDistance)
                MovetoNextPoint();
        }
    }
    public void MovetoNextPoint()
    {
        if (reachedToEnd)
            return;
        // Recalling the method if no points have been set up
        if (pathWay.Length <= 0)
        {
            Invoke(nameof(MovetoNextPoint), 0.1f);
            return;
        }

        // Reached to the end of the waypoints
        if (destPoint >= pathWay.Length)
        {
            animator?.SetTrigger("Attack");
            reachedToEnd = true;
            return;
        }

        // Set the agent to go to the currently selected destination.
        nMAgent.SetDestination(pathWay[destPoint].position);

        // Choosing the next point in the array as the next destination
        if (destPoint < pathWay.Length)
            destPoint++;
    }
}
