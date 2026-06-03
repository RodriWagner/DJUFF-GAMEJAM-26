using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Threading.Tasks;

public class RealityPostProcessing : MonoBehaviour
{
    [Header("Configuração de Tempo")]
    [Tooltip("Tempo, em segundos, para a transição acontecer")]
    public float fadeDurantion = 0.8f;
    private Volume myVolume;
    private Coroutine fadeCoroutine;

    void Start()
    {
        myVolume = GetComponent<Volume>();
        if (RealityManager.Instance != null)
        {
            await RealityManager.Instance.onRealityChanged.AddListener();

        }
    }

}
