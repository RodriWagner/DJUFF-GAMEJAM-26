using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RealityManager : MonoBehaviour //MANUTENÇÃO GLOBAL DA TROCA DE REALIDADE
{

    public static RealityManager Instance; //instanciar o script
    public enum RealityType { BlackAndWhite, Colorful } //enum das duas realidades

    [Header("Realidade Inicial do Jogo")]
    public RealityType currentReality = RealityType.BlackAndWhite; //inicia em blackAndwhite

    [Header("Objetos (Lista Automática)")]
    [Tooltip("Não é necessário adicionar manualmente, todos os objetos são adicionados aqui em suas funções de 'Start'!")]
    public UnityEvent onRealityChanged;

    void Awake()
    {
        //configurar a instancia
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ChangeReality(InputAction.CallbackContext context)
    {
        if (context.started)
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
