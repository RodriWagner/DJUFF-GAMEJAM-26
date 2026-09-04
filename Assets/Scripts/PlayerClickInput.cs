using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClickInput : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    //Chamado no evento "Interact" do inputSystem
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 clickedPoint = mainCamera.ScreenToWorldPoint(mousePos);

        Collider2D clickedCollider = DetectCollision();

        Interactable interactable = null;

        if (clickedCollider != null) clickedCollider.TryGetComponent<Interactable>(out interactable);

        interactionExecuted = false;
        agent.isStopped = false;

        //LOGICA PARA DETECTAR "QUAL CLIQUE" FOI FEITO (pra interagir ou somente pra andar?)

        if (interactable != null){
            //o destino é um puzzle (para antes)
            targetObject = clickedCollider;
            destine = interactable.transform.position;
            agent.stoppingDistance = interactionRange;
        }
        else{
            //o destino é um ponto no chao
            targetObject = null;
            destine = clickedPoint;
            agent.stoppingDistance = moveToRange;
        }

        //Capta o ponto valido mais proximo
        if (!TryFindValidDestination(ref destine)) //obs: ref passa como referencia (ponteiro)
        {
            CancelMovement();
            return;
        }

        hasDestination = agent.SetDestination(destine); //calcula o caminho ate o destino e retona se é valido ou nao

        if (!hasDestination) CancelMovement();
    }

}