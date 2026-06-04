using UnityEngine;

public class LevelTransition : Interactable
{
    [Tooltip("Numero da proxima sala")] public int nextRoom;
    [Tooltip("Player")] public GameObject player;
    public override void Action()
    {
        base.Action();
        mainCamera.transform.position = new Vector2(0, nextRoom * 100);
        player.transform.position = new Vector2(0, nextRoom * 100);
    }
}
