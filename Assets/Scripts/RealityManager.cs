using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RealityManager : MonoBehaviour
{
    public static RealityManager Instance; //instanciar o script
    public enum RealityType { BlackAndWhite, Colorful }
    public RealityType currentReality = RealityType.BlackAndWhite; //iniciar blackAndwhite
    public UnityEvent onRealityChanged;

    void Awake()
    {
        //configurar a instancia
        Debug.Log("awake");
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ChangeReality(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //logica de inversao
            Debug.Log("changereality");
            if (currentReality == RealityType.BlackAndWhite)
                currentReality = RealityType.Colorful;
            else
                currentReality = RealityType.BlackAndWhite;

            //invoke da troca de realidade (avisar globalmente)
            if (onRealityChanged != null)
                onRealityChanged.Invoke();
        }

    }

}
