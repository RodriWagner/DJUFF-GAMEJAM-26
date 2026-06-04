using UnityEngine;
public class LevelTransition : Interactable
{
    [Header("Configuração da Sala")]
    [Tooltip("Target da posição da próxima sala")] [SerializeField] private GameObject roomPosition;

    [Header("Configuração do Player")]
    [Tooltip("Objeto do player")] [SerializeField] private GameObject player;
    [Tooltip("Target da posição inicial do player na próxima sala")] [SerializeField] private GameObject playerNextPosition;
    public override void Action()
    {
        base.Action();
        if (player.TryGetComponent<PlayerMoviment>(out PlayerMoviment script))
        {
            script.destino = new Vector2(playerNextPosition.transform.position.x, playerNextPosition.transform.position.y);
        }

        mainCamera.transform.position = new Vector3(roomPosition.transform.position.x, roomPosition.transform.position.y, -10);
        player.transform.position = new Vector2(playerNextPosition.transform.position.x, playerNextPosition.transform.position.y);
    }
}
