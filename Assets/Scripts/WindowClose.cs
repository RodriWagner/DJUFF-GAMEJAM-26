using UnityEngine;

public class WindowClose : MonoBehaviour
{
    [Tooltip("Manager")] public GameObject pai;
    [Tooltip("Manager")] public GameObject pai2;
    [Tooltip("Player")] public GameObject player;
    public void Close()
    {
        if (pai.TryGetComponent<Interactable>(out Interactable ActionObject))
        {
            if (pai2.TryGetComponent<Interactable>(out Interactable ActionObject2))
            {
                ActionObject.ExitZoom();
                ActionObject2.ExitZoom();
            }
            if (player.TryGetComponent<PlayerMoviment>(out PlayerMoviment script))
            {
                script.canMove = true;
            }
        }
    }
}
