using UnityEngine;

public class TonalLadderGerenciator : MonoBehaviour
{
    [Header("Todos os Anéis")]
    public RingDrag[] allRings;

    public void checkVictory()
    {
        foreach (RingDrag ring in allRings)
        {
            //se algum deles estiver FORA do lugar, retorna
            if (!ring.isOnCorrectHolder())
                return;
        }
        Debug.Log("PUZZLE TA CORRETO AEEE");
    }
}
