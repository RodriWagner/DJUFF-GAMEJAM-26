using UnityEngine;

public class GameEnd : Interactable
{
    public override void Action()
    {
        Application.Quit();
    }
}
