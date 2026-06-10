using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HanoiRealityGerenciator : MonoBehaviour
{
    [Header("Hastes do Hanoi [EM ORDEM]")]
    [Tooltip("Coloque todas as hastes em ordem (esquerda = 0")] public List<HanoiStem> allStems;
    [Header("Discos do Hanoi [ORDEM INICIAL]")]
    [Tooltip("Coloque todos os discos do MAIOR pro MENOR (maior disco (da base) = índice 0)")] public List<HanoiDisk> initialOrder;
    [Header("Ordem Correta [FINAL]")]
    [Tooltip("Coloque a ordem certa para o puzzle estar correto (disco da base = índice 0)")] public List<HanoiDisk> correctOrder;
    void Start()
    {
        InitialSetter();
    }
    public void InitialSetter() //reinicia (ou inicia) o puzzle
    {
        HanoiStem leftStem = allStems[0]; //pegar a esquerda (facilitar a leitura do codigo)
        //limpa as listas
        for (int i = 0; i < allStems.Count; i++)
            allStems[i].disksHere.Clear();

        for (int i = 0; i < initialOrder.Count; i++)
        {
            HanoiDisk disk = initialOrder[i]; //pega o disco atual (facilitar a leitura do codigo)
            //coloca na esquerda
            disk.currentStem = leftStem;
            leftStem.disksHere.Add(disk);

            //coloco na posicao mais alta disponivel (calculada pelo TopPosition)
            RectTransform rectDisk = disk.GetComponent<RectTransform>();
            //obs: count = i pois preciso alocar exatamente nas posicoes que o i dita
            rectDisk.anchoredPosition = leftStem.TopPosition(i);
        }
    }
    public bool CheckRealityVictory() //checar vitoria de apenas apenas dessa realidade
    {
        HanoiStem rightStem = allStems[2]; //pegar a direita (facilitar a leitura do codigo)
        if (rightStem.disksHere.Count == correctOrder.Count) //se a haste da direita tiver a msm quantidade que a ordem correta
        {
            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (rightStem.disksHere[i] != correctOrder[i])
                    return false; //se houver algum fora da ordem, eh falso
            }
            Debug.Log("PUZZLE CORRETO NA REALIDADE: " + RealityManager.Instance.currentReality);
            return true;
        }
        return false; //se a quantidade for diferente, ja eh falso
    }
}
