using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [Tooltip("Objeto muda algo na fase")] public bool Interactive;
    [Tooltip("Objeto mostra um texto na tela")] public bool Informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    [Tooltip("Objeto pode ser ampliado")] public bool Zoom;
    public void action()
    {
        Debug.Log("Fiz algo");
    }
    public void text()
    {
        Debug.Log("Mostrei algo na tela");
        Debug.Log(message);
    }
    public void zoom()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
    }
}
