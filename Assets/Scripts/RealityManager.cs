using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RealityManager : MonoBehaviour
{
    public static RealityManager Instance; //instanciar o script
    public enum RealityType { BlackAndWhite, Colorful }
    public RealityType currentReality = RealityType.BlackAndWhite;
    public UnityEvent onRealityChanged;

    void Awake()
    {
        //configurar a instancia
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void ChangeReality(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //logica de inversao
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
