using UnityEngine;

public class RealityPlayer : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM AMBAS AS REALIDADES, MAS O GAMEplayer EM SI MUDA NELES
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
        playerAnimation.SetTrigger("Transition");
    }
    void Destroy()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.RemoveListener(UpdateObject);
        }
    }
}
