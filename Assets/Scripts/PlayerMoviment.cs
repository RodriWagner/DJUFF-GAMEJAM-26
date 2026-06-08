using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    public float velocidade = 5f;

    public Vector2 destino;
    private Camera mainCamera;
    public bool canMove = true;
    private Collider2D targetObject = null;

    [SerializeField] private float interaction_range = 3.5f;
    private Animator anim;

    private void Awake()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        anim.SetFloat("Direction", 1f);
    }
    private void FixedUpdate()
    {
        MovePlayer();
        CheckDirection();
        if (targetObject != null)
        {
            DetectRange(targetObject);
        }
        
    }
    private void OnInteract(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            if (!canMove) return;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            destino = mainCamera.ScreenToWorldPoint(mousePos);
            targetObject = DetectCollision();
        }
    }
    private void MovePlayer()
    {
        if (RealityManager.Instance.cooldownTime > 0.0f)
        {
            return;
        }
        //MOVIMENTA O PLAYER
        if ((Vector2)transform.position != destino)
        {
            anim.SetBool("Moving", true);
            // MoveTowards move de um ponto A para um ponto B em linha reta, numa velocidade constante
            transform.position = Vector2.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
        }
        else anim.SetBool("Moving", false);
    }
    private void CheckDirection()
    {
        float diffX = destino.x - transform.position.x;
        float diffY = destino.y - transform.position.y;

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
            
            anim.SetFloat("Direction", -dirY); 
        }
        else
        {
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", 0f);
        }
    }
    private Collider2D DetectCollision()
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();
        Ray raio = mainCamera.ScreenPointToRay(MousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(raio); //detecta que o mouse esta por cima de algum objeto
        return hit.collider;
    }
    private void DetectRange(Collider2D target)
    {
        float obj_distance = Vector2.Distance(transform.position, target.transform.position);
        if(obj_distance > interaction_range)
        {
            Debug.Log("Objeto muito longe para ser interagido.");
            return;
        }
        DoAction(target);
        targetObject = null;
    }
    private void DoAction(Collider2D target)
    {
        if (target.gameObject.TryGetComponent<Interactable>(out Interactable ActionObject)) //detecta se o objeto e o mesmo que tem o script
        {
            if (ActionObject.interactive) ActionObject.Action();
            if (ActionObject.informative) ActionObject.ShowText();
            if (ActionObject.zoom)
            {
                ActionObject.Amplify();
                canMove = false;
            }
        }
        //USANDO SO PRA PORTA
        if (target.gameObject.TryGetComponent<WindowClose>(out WindowClose Script))
        {
            Script.Close();
            canMove = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        destino = transform.position;
        anim.SetBool("Moving", false);
    }
}
