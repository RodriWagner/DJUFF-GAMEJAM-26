using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    public float velocidade = 5f;

    public Vector2 destino;
    private Camera mainCamera;
    public bool canMove = true;
    private bool moving = false;

    [SerializeField] private float interaction_range = 3.5f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        if (moving)
        {
            DetectCollision();
        }
    }
    private void OnInteract(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            if (!canMove) return;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            destino = mainCamera.ScreenToWorldPoint(mousePos);
        }
    }
    private void MovePlayer()
    {
        if (RealityManager.Instance.cooldownTime > 0.0f)
        {
            moving = false;
            return;
        }
        //MOVIMENTA O PLAYER
        if ((Vector2)transform.position != destino)
        {
            // MoveTowards move de um ponto A para um ponto B em linha reta, numa velocidade constante
            transform.position = Vector2.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
            moving = true;
        }
        else moving = false;
    }
    private void DetectCollision()
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();
        Ray raio = mainCamera.ScreenPointToRay(MousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(raio); //detecta que o mouse esta por cima de algum objeto
        if (hit.collider != null) //detecta que o objeto tem um colisor
        {
            DetectRange(hit.collider);
        }
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
    }
}
