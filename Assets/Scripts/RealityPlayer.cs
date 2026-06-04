using UnityEngine;

public class RealityPlayer : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM AMBAS AS REALIDADES, MAS O GAMEplayer EM SI MUDA NELES

    [Header("Player das Realidades")]
    [Tooltip("Arraste o player do Preto e Branco aqui")]
    public GameObject playerBlackAndWhite;
    [Tooltip("Arraste o player do Colorido aqui")]
    public GameObject playerColorful;

    [Header("Animação de Transição")]
    public Animator playerAnimation;

    void Start()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(UpdateObject);
            OnRealityChanged();
        }
    }

    void OnRealityChanged()
    {
        UpdateObject(); //atualizar o objeto do player
        
        //ATIVAR ANIMACAO

        //ATIVAR O SOM

        //congelar movimento ja é feito no PlayerMoviment
    }
    void UpdateObject()
    {
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            if (playerBlackAndWhite != null) playerBlackAndWhite.SetActive(true);
            if (playerColorful != null) playerColorful.SetActive(false);
        }
        else
        {
            if (playerBlackAndWhite != null) playerBlackAndWhite.SetActive(false);
            if (playerColorful != null) playerColorful.SetActive(true);
        }
    }
    void Destroy()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.RemoveListener(UpdateObject);
        }
    }
}
