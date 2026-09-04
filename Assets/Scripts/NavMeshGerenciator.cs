using System.Drawing;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using NUnit.Framework;
using System.Security.Cryptography;
using Unity.VisualScripting;

public class NavMeshGerenciator : MonoBehaviour
{
    [Header("Moviment")]
    [SerializeField] private float moveToRange = 0.1f;
    [SerializeField] private float maxDistanceToFindSamplePoint = 1.0f;
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 1.2f;

    //==[Captadas no Start]==
    private Camera mainCamera;
    private Animator anim;
    private NavMeshAgent agent;
    
    //==[Variaveis]==
    private bool hasDestination;
    private bool interactionExecuted;
    private bool canMove = true;
    private Vector2 destine;
    private Collider2D targetObject = null;

    void Start()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = moveToRange;
        agent.isStopped = true;
    }

    void FixedUpdate()
    {
        UpdateMoviment();
        UpdateDiraction();
    }


    //Chamado no evento "Interact" do inputSystem
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started || !canMove) return;
        
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


    private void UpdateMoviment()
    {
        if (!hasDestination)
        {
            SetMovingAnimation(false);
            return;
        }

        if (RealityManager.Instance.isInCooldown()){
            agent.isStopped = true;
            SetMovingAnimation(false);
            return;
        }

        if (agent.pathPending){
            SetMovingAnimation(false);
            return;
        }

        if (agent.pathStatus != NavMeshPathStatus.PathComplete){
            CancelMovement();
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance){
            SetMovingAnimation(false);
            if (targetObject == null){
                
            }
        }

        bool isMoving = agent.pathPending ||
                        (agent.hasPath && agent.remainingDistance > agent.stoppingDistance);
        anim.SetBool("Moving", isMoving);
        if (!isMoving)
        {
            agent.SetDestination(destine);
        }
    }


    //Encontrar se existe (e qual é) o ponto valido mais proximo do clicado
    private bool TryFindValidDestination(ref Vector2 destine){
        NavMeshHit navMeshHit; //variavel pra guardar infos sobre o ponto

        bool pointIsValid = NavMesh.SamplePosition(destine, out navMeshHit, maxDistanceToFindSamplePoint, NavMesh.AllAreas); //SamplePosition encontra a posicao valida mais proxima do destine

        //se nao for um ponto valido na malha, "cancela" o click
        if (!pointIsValid) return false;

        destine = navMeshHit.position; //substitui o ponto de clique pela posicao valida proxima encontrada no SamplePosition
        return true;
    }

    private Collider2D DetectCollision()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray); //detecta que o mouse esta por cima de algum objeto
        return hit.collider;
    }

        private void CheckDirection()
    {
        float diffX = destine.x - transform.position.x;
        float diffY = destine.y - transform.position.y;

        // qual eixo tem o movimento mais forte
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
        {
            // O MOVIMENTO DOMINANTE É NA HORIZONTAL
            float dirX = diffX > 0 ? 1f : -1f;
            anim.SetFloat("Horizontal", dirX);
            anim.SetFloat("Vertical", 0f);
        }
        else if (Mathf.Abs(diffY) > Mathf.Abs(diffX))
        {
            // O MOVIMENTO DOMINANTE É NA VERTICAL
            float dirY = diffY > 0 ? 1f : -1f;
            anim.SetFloat("Vertical", dirY);
            anim.SetFloat("Horizontal", 0f);
        }
        else
        {
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", 0f);
        }
    }

    private void UpdateDiraction()
    {
        if (targetObject == null || 
            interactionExecuted || 
            agent.pathPending || 
            !agent.hasPath)
        {
            return;
        }
        
        //path.complete retorna true se existir um caminho inteiro valido, caso nao haja, cancela o movimento
        if (agent.pathStatus != NavMeshPathStatus.PathComplete){
            CancelMovement();
            return;
        }

        if (agent.remainingDistance > agent.stoppingDistance){
            return;
        }
        agent.isStopped = true;
        anim.SetBool("Moving", false);

        Interactable interactable = targetObject.GetComponent<Interactable>();
        
        if (interactable.interactive)
        {
            interactable.Action();
        }

        if (interactable.informative)
        {
            interactable.ShowText();
        }

        if (interactable.zoom)
        {
            Debug.Log(interactable.zoom);
            interactable.Amplify();
            canMove = false;
        }

        //USANDO SO PRA PORTA
        if (targetObject.gameObject.TryGetComponent<WindowClose>(out WindowClose Script))
        {
            Script.Close();
            canMove = true;
        }

        interactionExecuted = true;
        targetObject = null;
        hasDestination = false;
    }

    public void CancelMovement()
    {
        agent.isStopped = true;
        agent.ResetPath();

        hasDestination = false;
        interactionExecuted = false;
        targetObject = null;
        
        SetMovingAnimation(false);
    }
    
    //Apenas para modularizacao
    private void SetMovingAnimation(bool isMoving)
    {
        anim.SetBool("Moving", isMoving);
    }
}

