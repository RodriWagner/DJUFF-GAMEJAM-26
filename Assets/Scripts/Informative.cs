using UnityEngine;

public class Informative : MonoBehaviour
{
    [Tooltip("Objeto mostra um texto na tela")] public bool informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    public void ShowText()
    {
        Debug.Log("Mostrei algo na tela");
        Debug.Log(message);
    }
}
