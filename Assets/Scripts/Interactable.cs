using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Tooltip("Objeto muda algo na fase")] public bool interactive;
    [Tooltip("Objeto mostra um texto na tela")] public bool informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    [Tooltip("Caixa de texto(UI)")] public GameObject textUI;
    [Tooltip("Caixa de texto")] public TMP_Text textBox;
    [Tooltip("Tempo da mensagem na tela")] public float timer;
    [Tooltip("Objeto pode ser ampliado")] public bool zoom;
    private bool timerStart = false;
    private float timerAux = 0;
    private void Update()
    {
        if (timerStart)
        {
            timerAux += Time.deltaTime;
            if (timerAux >= timer)
            {
                textUI.SetActive(false);
                timerAux = 0;
                timerStart = false;
            }
        }
    }
    public virtual void Action()
    { // FAZER UM IF DEPENDENDO DA REALIDADE PARA QUE O OBJ TENHA 2 FUNCOES DIFERENTES
        Debug.Log("Fiz algo");
    }
    public virtual void ShowText()
    {
        Debug.Log("Mostrei algo na tela");
        Debug.Log(message);
        textUI.SetActive(true);
        textBox.text = message;
        timerStart = true;
    }
    public virtual void Amplify()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
    }
}
