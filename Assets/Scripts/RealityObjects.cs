using UnityEngine;

public class RealityObjects : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM AMBAS AS REALIDADES, MAS O GAMEOBJECT EM SI MUDA NELES

    [Header("Objetos das Realidades")]
    [Tooltip("Arraste os filhos de cada realidade desse objeto aqui")]
    public GameObject objectBlackAndWhite;
    public GameObject objectColorful;

    void Start()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(UpdateState);
            UpdateState();
        }
    }

    void UpdateState()
    {
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            if (objectBlackAndWhite != null) objectBlackAndWhite.SetActive(true);
            if (objectColorful != null) objectColorful.SetActive(false);
        }
        else
        {
            if (objectBlackAndWhite != null) objectBlackAndWhite.SetActive(false);
            if (objectColorful != null) objectColorful.SetActive(true);
        }
    }
    void Destroy()
    {
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.RemoveListener(UpdateState);
        }
    }
}
