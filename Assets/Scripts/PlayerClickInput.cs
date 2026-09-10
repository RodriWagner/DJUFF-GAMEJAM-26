using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClickInput : MonoBehaviour
{
    //===[REFERENCIAS]===
    private Camera mainCamera;
    private PlayerNavMeshMovement movement;

    [Header("Interacoes")]
    [Tooltip("Layer das Interações")]
    [SerializeField] private LayerMask clickableLayers;
    [SerializeField] private float interactionDistance = 1.2f;
    private Interactable pendingInteraction;

    private void Awake()
    {
        mainCamera = Camera.main;
        movement = GetComponent<PlayerNavMeshMovement>();
    }
    
    void Update()
    {
        CheckPendingInteraction();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        CancelPendingInteraction(); //se houve um novo clique, a interação pendente é cancelada

        Ray mouseRay = GetMouseRay();

        if (TryGetClickedObject(mouseRay, out Collider2D collider))
        {
            OnClickedObject(collider);
            return;
        }

        if (TryGetGroundPosition(mouseRay, out Vector3 groundPosition))
            movement.TryMoveTo(groundPosition, movement.DefaultStoppingDistance);
    }

    private bool TryGetClickedObject(Ray mouseRay, out Collider2D collider)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(mouseRay, Mathf.Infinity, clickableLayers);
        collider = hit.collider;

        return collider != null;
    }

    private void OnClickedObject(Collider2D collider)
    {
        Debug.Log("INTERAGIVEL");
        Interactable interactable =
            collider.GetComponentInParent<Interactable>();

        if (interactable == null)
            return;

        Vector3 targetPosition = interactable.transform.position;

        bool canGoToPuzzle = movement.TryMoveTo(targetPosition, interactionDistance);

        if(!canGoToPuzzle)
            return;
        
        pendingInteraction = interactable;
    }
    
    //Checa se a interação pendente ja pode ser executada
    private void CheckPendingInteraction()
    {
        if (pendingInteraction == null)
            return;

        if (!movement.HasReachedDestination())
            return;
        
        ExecutePendingInteraction();
    }

    //Executa a interação pendente
    private void ExecutePendingInteraction()
    {
        Interactable interactable = pendingInteraction;
        CancelPendingInteraction(); //reseta a interacao que estava pendente

        if (interactable.interactive)
            interactable.Action();

        if (interactable.informative)
            interactable.ShowText();

        if (interactable.zoom)
            interactable.Amplify();
    }

    private void CancelPendingInteraction()
    {
        pendingInteraction = null;
    }
    private bool TryGetGroundPosition(Ray mouseRay, out Vector3 groundPosition)
    {
        Plane playerPlane = new Plane(Vector3.forward, transform.position);
        if (!playerPlane.Raycast(mouseRay, out float distance))
        {
            groundPosition = Vector3.zero;
            return false;
        }
        
        groundPosition = mouseRay.GetPoint(distance);
        return true;
    }

    private Ray GetMouseRay()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenPointToRay(mousePosition);
    }
}