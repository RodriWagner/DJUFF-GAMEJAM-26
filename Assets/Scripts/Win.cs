using UnityEngine;

public class Win : Interactable
{
    [Header("Gerenciador de cada Realidade")]
    public HanoiRealityGerenciator gerenciatorColorful;
    public HanoiRealityGerenciator gerenciatorBlackAndWhite;
    public GameObject oldUI;
    public override void Amplify()
    {
        if (gerenciatorBlackAndWhite.RealityWin && gerenciatorColorful.RealityWin)
        {
            base.Amplify();
        }
    }
}
