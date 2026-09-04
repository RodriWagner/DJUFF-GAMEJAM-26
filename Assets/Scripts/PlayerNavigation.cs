using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavigation : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementStoppingDistance = 0.1f;

    [Header("Interaction")]
    [SerializeField] private float interactionStoppingDistance = 1.2f;

    //==[Captadas no Awake]==
    private NavMeshAgent agent;
    private NavMeshDestinationValidator destinationValidator;

    //==[Variáveis]==
    private bool hasDestination;
    private bool movementEnabled = true;

    //==[Getters]==
    public bool MovementEnabled => movementEnabled;
    public bool HasDestination => hasDestination;
    public Vector3 Destination => agent.destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        destinationValidator = GetComponent<NavMeshDestinationValidator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
        agent.stoppingDistance = movementStoppingDistance;
    }

    private void Update()
    {
        if (!hasDestination) return;

        if (!movementEnabled ||
            RealityManager.Instance.isInCooldown() ||
            agent.pathStatus != NavMeshPathStatus.PathComplete ||
            agent.remainingDistance <= agent.stoppingDistance)
        {
            CancelMovement();
            return;
        }

        agent.isStopped = false;
    }

        public void CancelMovement()
    {
        agent.isStopped = true;
        agent.ResetPath();
        hasDestination = false;
    }
    
    public bool TryMoveTo(Vector3 requestedPosition, bool isInteraction)
    {
        if (!movementEnabled) return false;
        if (destinationValidator == null)
        {
            CancelMovement();
            return false;
        }
    }

}