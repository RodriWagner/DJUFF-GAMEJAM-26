using System.Collections.Generic;
using UnityEngine;
public class HanoiStem : MonoBehaviour
{
    [Header("Lista dos discos empilhados nesse momento")]
    public List<HanoiDisk> disksHere = new List<HanoiDisk>();

    [Header("Distância no Y entre cada Disco")]
    public float distanceInY = 3.0f;

    public bool IsOnTop(HanoiDisk disk) //verificar se esse disco é o do topo
    {
        Debug.Log("verificando se esta no topo");
        if (disksHere.Count == 0) return false; //se nao houver discos, nao tem nenhum no topo
        if (disksHere[disksHere.Count - 1] == disk) return true; //se for o mesmo que o ultimo, é o do topo
        return false; //se nao, nao ta no topo
    }

    public Vector2 TopPosition(float count) //calcula a posicao atual do topo (pra fixar o disco)
    {
        //obs: essa funcao aparece 3 vezes, mas com o "count" diferente em cada uma, por isso
        //modularizei ela aqui
        RectTransform myReact = GetComponent<RectTransform>();
        //calcula o quanto a altura deve subir
        float upPos = count * distanceInY;
        //retorna um vetor com a base do X (que nao muda), e o Y somado à altura necessária
        return new Vector2(myReact.anchoredPosition.x, myReact.anchoredPosition.y + upPos);
    }
}
