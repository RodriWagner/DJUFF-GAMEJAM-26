using System.Drawing;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using NUnit.Framework;
using System.Security.Cryptography;

public class NavMeshGerenciator : MonoBehaviour
{
    [SerializeField] private Vector2 destine;
    [SerializeField] private float interactionRange = 1.2f;
    [SerializeField] private float moveToRange = 0.1f;
    [SerializeField] private float maxDistanceToFindSamplePoint = 1.0f;

    //==[Captadas no Start]==
    private Camera mainCamera;
    private Animator anim;
    private NavMeshAgent agent;
    
    //==[Variaveis]==
    private bool hasDestination;
    private bool interactionExecuted;
    private bool canMove = true;
    private Collider2D targetObject = null;

    void Start()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        MovePlayer();
        CheckDirection();
        CheckInteraction();
    }
    
    //Chamado no evento "Interact" do inputSystem
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started || !canMove)return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 clickPoint = mainCamera.ScreenToWorldPoint(mousePos);

        targetObject = DetectCollision();
        interactionExecuted = false;

        //LOGICA PARA DETECTAR "QUAL CLIQUE" FOI FEITO (pra interagir ou somente pra andar?)

        if (targetObject != null &&
            targetObject.TryGetComponent<Interactable>(out Interactable interactable))
        {
            //o destino é um puzzle (para antes)
            destine = targetObject.transform.position;
            agent.stoppingDistance = interactionRange;
        }
        else{
            //o destino é um ponto no chao
            targetObject = null;
            agent.stoppingDistance = moveToRange;
            destine = clickPoint;
        }

        //LOGICA PARA ENCONTRAR O PONTO VALIDO MAIS PROXIMO DO CLICADO

        NavMeshHit navMeshHit; //variavel pra guardar infos sobre o ponto

        bool pointIsValid = NavMesh.SamplePosition(destine, out navMeshHit, maxDistanceToFindSamplePoint, NavMesh.AllAreas); //SamplePosition encontra a posicao valida mais proxima do destine
        //se nao for um ponto valido na malha, "cancela" o click
        if (!pointIsValid){
            hasDestination = false;
            agent.ResetPath(); //remove o caminho atual
            return;
        }
        destine = navMeshHit.position; //substitui o ponto de clique pela posicao valida proxima encontrada no SamplePosition
        hasDestination = agent.SetDestination(destine); //calcula o caminho ate o destino e retona se é valido ou nao
    }

    private void MovePlayer()
    {
        //Verifica o cooldown de troca de mundos
        if (!hasDestination || RealityManager.Instance.isInCooldown())
        {
            anim.SetBool("Moving", false);
            return;
        }

        bool isMoving = agent.pathPending ||
                        (agent.hasPath && agent.remainingDistance > agent.stoppingDistance);
        anim.SetBool("Moving", isMoving);
        Debug.Log(isMoving);
        if (!isMoving)
        {
            agent.SetDestination(destine);
        }
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

    private void CheckInteraction()
    {
        if (targetObject == null || 
            interactionExecuted || 
            agent.pathPending || 
            !agent.hasPath)
        {
            return;
        }

        if (agent.pathStatus != NavMeshPathStatus.PathComplete){
            agent.ResetPath();
            hasDestination = false;
            targetObject = null;
            anim.SetBool("Moving", false);
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
            interactable.Amplify();
            canMove = false;
        }

        interactionExecuted = true;
        targetObject = null;
        hasDestination = false;
    }
}

