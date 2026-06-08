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
    private Animator playerAnimation;
    void Awake()
    {
        playerAnimation = GetComponent<Animator>();
    }
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
            playerAnimation.SetBool("Colorful", false);
        }
        else
        {
            playerAnimation.SetBool("Colorful", true);
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
