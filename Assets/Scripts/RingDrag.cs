using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class RingDrag : MonoBehaviour
{
    [Header("Todos do Encaixes")]
    [Tooltip("TODOS os holders do puzzle aqui")] public Transform[] allHolders;

    [Header("Holder correto desse Ring")]
    [Tooltip("Arraste apenas o holder que esse ring pertence")] public Transform targetPos;

    [SerializeField] private float distanceToFit = 1f;

    [Header("Áudio ao Clicar")]
    //AUDIO FMOD TOCAR
    
    [Header("Gerenciador do Puzzle")]
    public TonalLadderGerenciator puzzleGerence;

    
    private bool correct = false; //opcional caso TRAVE depois que vencer
    private RectTransform myRect;
    private Vector2 ringInicialPos;
    void Start()
    {
        //tem que ser pelo RECT TRANSFORM, pra trabalhar com a posicao na UI
        myRect = GetComponent<RectTransform>();
        //guardar a posicao ancorada inicial do ring pra retornar (caso solte)
        ringInicialPos = myRect.anchoredPosition;
    }

    public void CatchRing()
    {
        if (correct) return;
        if (RealityManager.Instance != null && RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            //AUDIO FMOD TOCAR
            Debug.Log("Toquei meu sonzinhooooo ding ding!");
        }
    }
    public void DragRing() //ativada enquanto SEGURA o ring
    {
        if (correct) return; 
        //pega a posicao (com o NOVO inputsystem)
        Vector2 mousePos = Mouse.current.position.ReadValue();
        //transforma em relacao a camera
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //pega o vetor3 da posicao do mouse e usa apenas x e y (mantem o z que ja tem)
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }

    public void DropRing() //ativada ao SOLTAR o ring
    {
        if (correct) return;
        Transform nearestHolder = null;
        float shortestDistance = Mathf.Infinity;
        
        foreach(Transform holder in allHolders)
        {
            float distance = Vector2.Distance(transform.position, holder.position);
            if (distance < )
        }
        if (distance < lockDistance) //se a distancia for pequena, deixa correto e aloca na posicao em si
        {
            correct = true;
            transform.position = targetPos.transform.position;
        }
        else //se a distancia for grande, volta ele pra posicao inicial
        {
            myRect.anchoredPosition = ringInicialPos;
        }
    }
}
