using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class RealityPostProcessing : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    [Tooltip("Tempo, em segundos, para a transição acontecer")]
    public float fadeDuration = 0.8f;

    private Volume myVolume;

    //Variável de controle
    private float targetWeight;
    private bool isFading = false;

    void Start()
    {
        myVolume = GetComponent<Volume>();

        if (RealityManager.Instance != null)
        {
            //entra no grupo dos eventos
            RealityManager.Instance.onRealityChanged.AddListener(OnRealityChanged);

            //ja configura o inicial
            //se for blackAndWhite, o peso do filtro é 1 (total), se nao, o peso é 0 (cores padroes)
            targetWeight = (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite) ? 1f : 0f;
            myVolume.weight = targetWeight;
        }
    }

    void OnRealityChanged()
    {
        //define o alvo (ir pra 1 ou 0 dependendo da realidade)
        targetWeight = (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite) ? 1f : 0f;
        isFading = true;
    }

    void Update()
    {
        //so continua a conta enquanto a isFading for true
        if (isFading)
        {
            //divide a durancao em proporção (pra trabalhar com o mathf.movetowards, que da passos constantes)
            float velocidade = 1f / fadeDuration;
            myVolume.weight = Mathf.MoveTowards(myVolume.weight, targetWeight, velocidade * Time.deltaTime);

            //se o peso já chegou próximo ao alvo, desliga o fade
            if (Mathf.Abs(myVolume.weight - targetWeight) < 0.1f)
            {
                myVolume.weight = targetWeight; //sempre coloca no valor exto pra seguranca
                isFading = false;
            }
        }
    }

    void OnDestroy()
    {
        if (RealityManager.Instance != null)
            RealityManager.Instance.onRealityChanged.RemoveListener(OnRealityChanged);
    }
}