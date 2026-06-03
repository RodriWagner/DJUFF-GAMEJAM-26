using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    public float velocidade = 5f;
    
    private Vector2 destino;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //MOVIMENTA O PLAYER
        if ((Vector2)transform.position != destino)
        {
            // MoveTowards move de um ponto A para um ponto B em linha reta, numa velocidade constante
            transform.position = Vector2.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
        }
    }
    private void OnInteract(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            Vector2 MousePos = Mouse.current.position.ReadValue();
            Ray raio = mainCamera.ScreenPointToRay(MousePos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(raio); //detecta que o mouse esta por cima de algum objeto
            if (hit.collider != null) //detecta que o objeto tem um colisor
            {
                if (hit.collider.gameObject.TryGetComponent<Interactable>(out Interactable ActionObject) ) //detecta se o objeto e o mesmo que tem o script
                {
                    if (ActionObject.interactive) ActionObject.Action();
                    if (ActionObject.informative) ActionObject.ShowText();
                    if (ActionObject.zoom) ActionObject.Amplify();
                }
            }
        }
    }
    private void OnMove(InputAction.CallbackContext contexto)
    {
        if (contexto.started)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            destino = mainCamera.ScreenToWorldPoint(mousePos);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        destino = transform.position;
    }
}
