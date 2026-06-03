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
    private void Update()
    {
        if ((Vector2)transform.position != destino)
        {
            // MoveTowards move de um ponto A para um ponto B em linha reta, numa velocidade constante
            transform.position = Vector2.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
        }
    }
}
