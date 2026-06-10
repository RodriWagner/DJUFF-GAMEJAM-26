using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [Tooltip("Objeto muda algo na fase")] public bool interactive;
    [Header("Objeto de Texto")]
    [Tooltip("Objeto mostra um texto na tela")] public bool informative;
    [Tooltip("Texto a ser mostrado")] public string message;
    [Tooltip("Caixa de texto(UI)")] public GameObject textUI;
    [Tooltip("Caixa de texto")] public TMP_Text textBox;
    [Tooltip("Tempo da mensagem na tela")] public float timer;
    [Header("Objeto de Zoom ou sair do Zoom")]
    [Tooltip("Objeto pode ser ampliado")] public bool zoom;
    [Tooltip("Popup a ser ampliado")] public GameObject zoomedUI;
    [Tooltip("Painel padrao para o fade")] public Image fadeScreen;

    private bool timerStart = false;
    private float timerAux = 0;
    public Camera mainCamera;
    private float zoomIn = 4f;
    private bool zoomStart = false;
    private bool zoomEnd = false;
    private bool zoomAux = false;
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
            FadeIn();
        }
        if (zoomEnd)
        {
            FadeOut();
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
        zoomAux = false;
        zoomStart = true;
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOM");
    }
    public void ExitZoom()
    {
        zoomAux = true;
        zoomStart = true;
        Debug.Log("not zoom ;c");
    }
    public void FadeIn()
    {
        mainCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
        actualColor = fadeScreen.color.a + Time.deltaTime * zoomSpeed;
        fadeScreen.color = new Color(0, 0, 0, actualColor);
        if (mainCamera.orthographicSize <= zoomIn)
        {
            zoomStart = false;
            SetVisibility(zoomAux);
        }
    }
    public void SetVisibility(bool exit)
    {
        if (zoomedUI)
        {
        if (exit) zoomedUI.SetActive(false);
        else zoomedUI.SetActive(true);
        }
        zoomEnd = true;
    }
    public virtual void FadeOut()
    {
        mainCamera.orthographicSize += Time.deltaTime * zoomSpeed;
        actualColor = fadeScreen.color.a - Time.deltaTime * zoomSpeed;
        fadeScreen.color = new Color(0, 0, 0, actualColor);
        if (actualColor <= 0)
        {
            mainCamera.orthographicSize = 5f;
            zoomEnd = false;
        }
    }
}
