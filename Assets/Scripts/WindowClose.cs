using UnityEngine;

public class WindowClose : MonoBehaviour
{
    [Tooltip("Manager")] public GameObject pai; 
    [Tooltip("Player")] public GameObject player;
    public void Close()
    {
        if (pai.TryGetComponent<Interactable>(out Interactable ActionObject))
        {
            ActionObject.ExitZoom();
            if (player.TryGetComponent<PlayerMoviment>(out PlayerMoviment script))
            {
                script.canMove = true;
            }
        }
    }
}
