using Unity.VisualScripting;
using UnityEngine;

public class RealityObject : MonoBehaviour
{
    [Header("Which reality belogns?")]
    public RealityManager.RealityType myReality;

    private Renderer myrenderer;
    private Collider mycollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("start");
        myrenderer = GetComponent<Renderer>();
        mycollider = GetComponent<Collider>();
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(UpdateVisibility);
        }
    }

    void UpdateVisibility()
    {
        Debug.Log("updatevisibility");
        //boleana pra saber se deveria aparecer ou nao
        bool check_reality = (RealityManager.Instance.currentReality == myReality);
        //tratamento de null pra caso nao tenha colisao ou renderer
        if (myrenderer != null) myrenderer.enabled = check_reality;
        if (mycollider != null) mycollider.enabled = check_reality;
    }

    void Destroy()
    {
        Debug.Log("deestroy");
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.RemoveListener(UpdateVisibility);
        }
    }
}
