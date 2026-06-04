using UnityEngine;

public class WindowClose : MonoBehaviour
{
    [Tooltip("Manager")] public GameObject pai;
    public void Close()
    {
        if (pai.TryGetComponent<Interactable>(out Interactable ActionObject))
        {
            ActionObject.ExitZoom();
        }
    }
}
