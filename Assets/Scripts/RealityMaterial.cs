using TMPro;
using UnityEngine;

public class RealityMaterial : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM AMBAS AS REALIDADES, MAS QUE ALGO MUDA NELES
    [Header("Material das Realidades")]
    public Material materialBlackAndWhite;
    public Material materialColorful;
    private Renderer myRenderer;
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(UpdateMaterial);
        }
    }
    void UpdateMaterial()
    {
        if (myRenderer != null)
        {
            if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
                myRenderer.material = materialBlackAndWhite;
            else
                myRenderer.material = materialColorful;
        }
    }
}
