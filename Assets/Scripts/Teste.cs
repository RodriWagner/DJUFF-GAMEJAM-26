using UnityEngine;

public class Teste : Interactable //SCRIPT MERAMENTE DE EXEMPLO PARA FAZER UMA FUNCAO QUE FUNCIONA DEPENDENDO DA REALIDADE
{
    public override void Action() //TIRAR O SCRIPT PAI DO OBJETO PARA QUE O OVERRIDE FUNCIONE
    {
        Debug.Log("script novoooo");
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite) // IF/ELSE FUNCIONOU
        {
            Debug.Log("estou cinzinha ;c");
        }
        else
        {
            Debug.Log("estou coloridah dyyvaaa");
        }
    }
}
