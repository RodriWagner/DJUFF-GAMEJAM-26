using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using SmoothShakeFree;

public class RealityManager : MonoBehaviour //MANUTENÇÃO GLOBAL DA TROCA DE REALIDADE
{
    public static RealityManager Instance; //instanciar o script
    public enum RealityType { BlackAndWhite, Colorful } //enum das duas realidades

    [Header("Realidade Inicial do Jogo")]
    public RealityType currentReality = RealityType.BlackAndWhite; //inicia em blackAndwhite

    [Header("Tempo de Cooldown para Transições")]
    [Tooltip("Tempo, em segundos, que o jogador deve esperar para trocar de realidade")]
    public float cooldownTime = 1.0f;

    [Header("Objetos (Lista Automática)")]
    [Tooltip("Não é necessário adicionar manualmente, todos os objetos são adicionados aqui em suas funções de 'Start'!")]
    public UnityEvent onRealityChanged; //"lista" dos objetos que precisam "saber" que a realidade mudou
    public SmoothShake cameraShake;
    private float ShakeTimer = 0f;
    private bool TimerTillShake = false;
    private bool onSwitch = true;

    void Awake()
    {
        //configurar a instancia
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        cooldownTime -= Time.deltaTime;
        if (TimerTillShake) ShakeTimer += Time.deltaTime;
        if (ShakeTimer >= (cameraShake.timeSettings.holdDuration / 2)) SwitchStart();
        if (ShakeTimer >= cameraShake.timeSettings.holdDuration) ShakeEnd();
    }

    public void ChangeReality(InputAction.CallbackContext context)
    {
        if (context.started) //se apertou a tecla
        {
            if (cooldownTime < 0.0f)
            {
                TimerTillShake = true;
                cameraShake.StartShake();
            }
        }
    }
    public void SwitchStart()
    {
        if (!onSwitch) return;
        //logica de inversao
        if (currentReality == RealityType.BlackAndWhite)
            currentReality = RealityType.Colorful;
        else
            currentReality = RealityType.BlackAndWhite;
        //invoke da troca de realidade (avisar globalmente)
        if (onRealityChanged != null)
            onRealityChanged.Invoke();
        onSwitch = false;
    }
    public void ShakeEnd()
    {
        onSwitch = true;
        TimerTillShake = false;
        cooldownTime = 1.0f;
        ShakeTimer = 0;
        cameraShake.StopShake();
    }
}
