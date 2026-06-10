using UnityEditor.EditorTools;
using UnityEngine;

public class Drawer : Interactable
{
    [Header("Gavetas")]
    [Tooltip("Objeto da gaveta enquanto esta fechada")] public GameObject ClosedDrawer;
    [Tooltip("Objeto da gaveta enquanto esta aberta")] public GameObject OpenedDrawer;
    [Tooltip("Define se essa gaveta esta ou nao aberta")] public bool isClosed;
    public void Swap()
    {
        if (isClosed)
        {
            Debug.Log("MUDEII");
            OpenedDrawer.SetActive(true);
            ClosedDrawer.SetActive(false);
        }
    }
    public override void FadeOut()
    {
        base.FadeOut();
        Swap();
    }
}
