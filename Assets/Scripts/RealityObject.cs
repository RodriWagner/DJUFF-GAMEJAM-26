using Unity.VisualScripting;
using UnityEngine;

public class RealityObject : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM APENAS UMA DAS REALIDADES

    [Header("Qual realidade pertence?")]
    public RealityManager.RealityType myReality;

    private Renderer myrenderer;
    private Collider mycollider;
    void Start()
    {
        //pegar as componentes no start
        myrenderer = GetComponent<Renderer>();
        mycollider = GetComponent<Collider>();
        //
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(UpdateVisibility);
            //aos curiosos: vou no script instanciado e dentro da caixa "onRealityChanged" adiciono esse objeto
            //quando o "onRealityChanged" for invocado ("invoke"), todos os objetos dentro dessa caixa
            //acionam a funcao que está passada em paramentro "UpdateVisibility"
        }
    }

    void UpdateVisibility()
    {
        //boleana pra saber se deveria aparecer ou nao
        bool check_reality = (RealityManager.Instance.currentReality == myReality);
        //tratamento de null pra caso nao tenha colisao ou renderer
        if (myrenderer != null) myrenderer.enabled = check_reality;
        if (mycollider != null) mycollider.enabled = check_reality;
    }

    void Destroy()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.RemoveListener(UpdateVisibility);
        }
    }
}
