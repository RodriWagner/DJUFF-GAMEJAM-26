using Unity.VisualScripting;
using UnityEngine;

public class HanoiCheck : MonoBehaviour
{
    [Header("Gerenciador de cada Realidade")]
    public HanoiRealityGerenciator gerenciatorColorful;
    public HanoiRealityGerenciator gerenciatorBlackAndWhite;
    public void CheckVictoryInBoth() //checa se ganhou nas DUAS realidades
    {
        //if (winPuzzle) return;
        if (gerenciatorColorful != null)
        {
            if (gerenciatorColorful.CheckRealityVictory())
                gerenciatorColorful.RealityWin = true;
            else
                gerenciatorColorful.RealityWin = false;
        }
        if (gerenciatorBlackAndWhite != null)
        {
            if (gerenciatorBlackAndWhite.CheckRealityVictory())
                gerenciatorBlackAndWhite.RealityWin = true;
            else
                gerenciatorBlackAndWhite.RealityWin = false;
        }
    }
}
