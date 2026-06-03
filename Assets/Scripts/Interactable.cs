using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [Tooltip("Objeto muda algo na fase")] public bool interactive;
    [Tooltip("Objeto mostra um texto na tela")] public bool informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    [Tooltip("Caixa de texto(UI)")] public GameObject textUI;
    [Tooltip("Caixa de texto")] public TMP_Text textBox;
    [Tooltip("Tempo da mensagem na tela")] public float timer;
    [Tooltip("Objeto pode ser ampliado")] public bool zoom;
    [Tooltip("Popup a ser ampliado")] public GameObject zoomedUI;
    [Tooltip("Painel padrao para o fade")] public Image fadeScreen;
    private bool timerStart = false;
    private float timerAux = 0;
    private Camera mainCamera;
    private float zoomIn = 4f;
    private bool zoomStart = false;
    private bool zoomEnd = false;
    private float zoomSpeed = 3f;
    private float actualColor;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
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
        if (zoomStart)
        {
            mainCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
            actualColor = fadeScreen.color.a + Time.deltaTime * zoomSpeed;
            fadeScreen.color = new Color(0, 0, 0, actualColor);
            if (mainCamera.orthographicSize <= zoomIn)
            {
                mainCamera.orthographicSize = 5f;
                zoomStart = false;
                zoomEnd = true;
            }
        }
        if (zoomEnd)
        {
            actualColor = fadeScreen.color.a - Time.deltaTime * zoomSpeed;
            fadeScreen.color = new Color(0, 0, 0, actualColor);
            zoomedUI.SetActive(true);
            if (actualColor <= 0) zoomEnd = false;
        }
    }
    public virtual void Action()
    {
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
    public void Amplify()
    {
        zoomStart = true;
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
    }
}
