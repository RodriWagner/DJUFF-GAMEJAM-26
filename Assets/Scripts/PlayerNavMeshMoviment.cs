using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavMeshMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float sampleDistance = 1f;

    //===[REFERENCIAS]===
    private Animator animator;
    private NavMeshAgent agent;

    //===[VARIAVEIS]===
    private bool hasDestination;
    private bool reachedDestination;

    //===[GETTERS]===
    public bool IsMoving => hasDestination;
    public NavMeshAgent Agent => agent;
    public float DefaultStoppingDistance => stoppingDistance;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        //config inicial do agent
        agent.updatePosition = true;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.stoppingDistance = stoppingDistance;
        agent.isStopped = true;
    }

    void Update()
    {
        UpdateMoviment();
        UpdateAnimation();
    }

    public bool TryMoveTo(Vector3 requestedPosition, float destinationStoppingDistance)
    {
        reachedDestination = false;

        if (!agent.isOnNavMesh)
        {
            CancelMovement();
            return false;
        }

        agent.stoppingDistance = destinationStoppingDistance;

        if (!TryFindCompletePath(requestedPosition, out Vector3 validPosition))
        {
            CancelMovement();
            return false;
        }

        if (!agent.SetDestination(validPosition))
        {
            CancelMovement();
            return false;
        }

        hasDestination = true;
        agent.isStopped = false;
        return true;
    }

    //Acha o ponto válido mais próximo ao clique E verifica se o player seria capaz de chegar la
    private bool TryFindCompletePath(Vector3 requestedPosition, out Vector3 validPosition)
    {
        validPosition = requestedPosition; //considerar o primeiro clique como valido

        //acha o ponto valido mais proximo possivel (obs: nao significa que o player consiga chegar la)
        float currentSampleDistance =
            Mathf.Max(sampleDistance, agent.stoppingDistance);

        if (!NavMesh.SamplePosition(
                requestedPosition,
                out NavMeshHit hit,
                currentSampleDistance,
                NavMesh.AllAreas))
            return false;
        
        //caso a simulacao seja possivel, passo o destino ao player 
        if (!SimulatePath(hit.position))
            return false;

        validPosition = hit.position;
        return true;
    }

    //"Simula" um caminho ate um ponto qualquer
    private bool SimulatePath(Vector3 point){
        NavMeshPath path = new NavMeshPath();
        bool pathCalculated = NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas, path);
        if (!pathCalculated || path.status != NavMeshPathStatus.PathComplete)
            return false;
        return true;
    }

    private void UpdateMoviment()
    {
        if (!hasDestination)
            return;

        if (RealityManager.Instance != null && RealityManager.Instance.IsInCooldown())
        {
            agent.isStopped = true;
            return;
        }

        if (!agent.isOnNavMesh)
        {
            CancelMovement();
            return;
        }

        if (agent.pathPending)
            return;

        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            CancelMovement();
            return;
        }

        if (HasReachedDestination())
        {
            agent.isStopped = true;
            agent.ResetPath();
            hasDestination = false;
            reachedDestination = true;
            return;
        }

        agent.isStopped = false;
    }
    
    public bool HasReachedDestination()
    {
         return reachedDestination ||
             (hasDestination &&
              !agent.pathPending &&
              agent.hasPath &&
              agent.pathStatus == NavMeshPathStatus.PathComplete &&
              agent.remainingDistance <= agent.stoppingDistance);
    }
    public void CancelMovement()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        hasDestination = false;
        reachedDestination = false;
        SetMovingAnimation(false);
    }

    private void UpdateAnimation()
    {
        Vector3 velocity = agent.velocity;

        if (velocity.sqrMagnitude < 0.001f)
            velocity = agent.desiredVelocity;

        bool isMoving = hasDestination && velocity.sqrMagnitude > 0.001f;

        SetMovingAnimation(isMoving);

        if (isMoving)
            UpdateDirection(velocity);
    }

    private void SetMovingAnimation(bool isMoving)
    {
        animator.SetBool("Moving", isMoving);
    }
    private void UpdateDirection(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            animator.SetFloat("Horizontal", Mathf.Sign(velocity.x));

            animator.SetFloat("Vertical", 0f);
            return;
        }

        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", Mathf.Sign(velocity.y));
    }

}