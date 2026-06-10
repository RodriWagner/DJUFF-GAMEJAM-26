using Unity.VisualScripting;
using UnityEngine;

public class HanoiCheck : MonoBehaviour
{
    [Header("Gerenciador de cada Realidade")]
    public HanoiRealityGerenciator gerenciatorColorful;
    public HanoiRealityGerenciator gerenciatorBlackAndWhite;

    private bool winPuzzle = false; //caso ganhar seja vitalicio
    private bool winC = false;
    private bool winBW = false;

    public void CheckVictoryInBoth() //checa se ganhou nas DUAS realidades
    {
        //if (winPuzzle) return;
        if (gerenciatorColorful != null)
        {
            if (gerenciatorColorful.CheckRealityVictory())
                winC = true;
            else
                winC = false;

        }

        if (gerenciatorBlackAndWhite != null)
        {
            if (gerenciatorBlackAndWhite.CheckRealityVictory())
                winBW = true;
            else
                winBW = false;

        }
        Debug.Log("BlackAndWhite: " + winBW + "Colorful: " + winC);
        if (winC && winBW)
        {
            //winPuzzle = true;
            WinHanoi();
        }
    }

    public void WinHanoi()
    {
        Debug.Log("GANHOU O JOGO TODOOO");
    }
}
