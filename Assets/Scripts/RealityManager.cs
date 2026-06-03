using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class RealityManager : MonoBehaviour //MANUTENÇÃO GLOBAL DA TROCA DE REALIDADE
{

    public static RealityManager Instance; //instanciar o script
    public enum RealityType { BlackAndWhite, Colorful } //enum das duas realidades

    [Header("Realidade Inicial do Jogo")]
    public RealityType currentReality = RealityType.BlackAndWhite; //inicia em blackAndwhite

    [Header("Tempo de Cooldown para Transições")]
    [Tooltip("Tempo, em segundos, que o jogador deve esperar para trocar de realidade")]
    [SerializeField] float cooldownTime = 1.0f;

    [Header("Objetos (Lista Automática)")]
    [Tooltip("Não é necessário adicionar manualmente, todos os objetos são adicionados aqui em suas funções de 'Start'!")]
    public UnityEvent onRealityChanged; //"lista" dos objetos que precisam "saber" que a realidade mudou

    void Awake()
    {
        //configurar a instancia
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        cooldownTime -= Time.deltaTime;
    }

    public void ChangeReality(InputAction.CallbackContext context)
    {
        if (context.started) //se apertou a tecla
        {
            if (cooldownTime < 0.0f)
            {
                //logica de inversao
                if (currentReality == RealityType.BlackAndWhite)
                    currentReality = RealityType.Colorful;
                else
                    currentReality = RealityType.BlackAndWhite;

                //invoke da troca de realidade (avisar globalmente)
                if (onRealityChanged != null)
                    onRealityChanged.Invoke();

                cooldownTime = 1.0f;
            }
        }
    }

}
