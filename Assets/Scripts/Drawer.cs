
using UnityEngine;

public class Drawer : Interactable
{
    [Header("Gavetas")]
    [Tooltip("Objeto da gaveta enquanto esta fechada")] public GameObject ClosedDrawerBW;
    [Tooltip("Objeto da gaveta enquanto esta fechada")] public GameObject ClosedDrawerC;
    [Tooltip("Objeto da gaveta enquanto esta aberta")] public GameObject OpenedDrawerBW;
    [Tooltip("Objeto da gaveta enquanto esta aberta")] public GameObject OpenedDrawerC;
    [Tooltip("Define se essa gaveta esta ou nao aberta")] public bool isClosed;
    public void Swap()
    {
        if (isClosed)
        {
            Debug.Log("MUDEII");
            OpenedDrawerBW.SetActive(true);
            OpenedDrawerC.SetActive(true);
            ClosedDrawerBW.SetActive(false);
            ClosedDrawerC.SetActive(false);
        }
    }
    public override void FadeOut()
    {
        base.FadeOut();
        Swap();
    }
}
