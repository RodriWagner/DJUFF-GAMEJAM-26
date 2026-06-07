using UnityEditor.Rendering;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.U2D.IK;
using UnityEngine.EventSystems;

public class HanoiDisk : MonoBehaviour
{
    [Header("Lista de Todos os Hastes")]
    public List<HanoiStem> allStems;
    [Header("Haste Atual (NÃO aloque nada)")]
    [Tooltip("HanoiGerenciator ja lida com a inicialização")] public HanoiStem currentStem;
    [Header("Hanoi Gerenciator")]
    public HanoiGerenciator gerenciator;

    public float distanceToFit = 1.0f;

    private RectTransform myRect;
    private bool canMove = false;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    public void CatchDisk() //ao clicar no disco (pointer UP)
    {
        //se for o do topo, pode mover
        if (currentStem != null && currentStem.IsOnTop(this))
            canMove = true;
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            Debug.Log("Toquei uma musiquinha");
            //AUDIO FMOD TOCAR
        }
    }

    public void DragDisk() //tentar arrastar o disco (DRAG)
    {
        if (canMove)
        {
            //mesma logica do RINGDRAG.dragRing
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        }
    }

    public void DropDisk() //logica parecida com a RINGDRAG.DropRing (pointer DOWN)
    {
        if (!canMove) return;
        canMove = false; //atualizar para nao poder arrastar

        //pegar a haste mais proxima E que ainda esteja na distancia minima pra dar FIT
        HanoiStem nearestStem = null;
        float shortestDistance = Mathf.Infinity;
        foreach (HanoiStem stem in allStems)
        {
            float distance = Vector2.Distance(transform.position, stem.transform.position);
            if (distance < distanceToFit && distance < shortestDistance)
            {
                nearestStem = stem;
                shortestDistance = distance;
            }
        }

        if (nearestStem != null) //se tiver achado uma haste, muda pra ela
        {
            currentStem.disksHere.Remove(this); //remove do haste que estava
            currentStem = nearestStem; //atualiza o haste atual

            //coloca na posicao do topo
            //obs: passo exatamente a quantidade de discos que tem, pois esse ira ocupar o lugar ("i = count") que está desocupado
            myRect.anchoredPosition = currentStem.TopPosition(currentStem.disksHere.Count);
            currentStem.disksHere.Add(this); //adiciona na haste atual
        }
        else
        {
            //aloca na haste antiga (que nao mudou)
            //obs: preciso remover 1 do total, pois esse esta "voando"
            myRect.anchoredPosition = currentStem.TopPosition(currentStem.disksHere.Count - 1);
        }


        //toda vez que soltar, verifica se esta tudo correto
        if (gerenciator != null)
            gerenciator.CheckVictory();
    }
}

