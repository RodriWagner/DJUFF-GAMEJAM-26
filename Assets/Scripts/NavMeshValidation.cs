using NavMeshPlus.Components.Editors;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDestinationValidator : MonoBehaviour
{
    [SerializeField] private float maxSampleDistance = 1f;

    public bool TryGetCompletePath(NavMeshAgent agent, Vector3 requestedPosition, out Vector3 validPosition)
    {
        validPosition = requestedPosition;

        if (agent == null || !agent.isOnNavMesh) return false;

        if (!NavMesh.SamplePosition(
                requestedPosition,
                out NavMeshHit hit,
                maxSampleDistance,
                NavMesh.AllAreas))
        {
            return false;
        }

        if (agent.pathStatus != NavMeshPathStatus.PathComplete) return false;

        validPosition = hit.position;
        return true;
    }
}