using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Tooltip("Objeto muda algo na fase")] public bool interactive;
    [Tooltip("Objeto mostra um texto na tela")] public bool informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    [Tooltip("Caixa de texto(UI)")] public GameObject textUI;
    [Tooltip("Caixa de texto")] public TMP_Text textBox;
    [Tooltip("Objeto pode ser ampliado")] public bool zoom;
    public void Action()
    {
        Debug.Log("Fiz algo");
        textUI.SetActive(false);
        // placeholder enquanto nao defino um timer para desabilitar a textbox
    }
    public void ShowText()
    {
        Debug.Log("Mostrei algo na tela");
        Debug.Log(message);
        textUI.SetActive(true);
        textBox.text = message;
    }
    public void Amplify()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
    }
}
