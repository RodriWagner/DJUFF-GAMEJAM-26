using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RingDrag : MonoBehaviour
{
    [Header("Todos do Encaixes")]
    [Tooltip("TODOS os holders do puzzle aqui")] public Transform[] allHolders;

    [Header("Holder correto desse Ring")]
    [Tooltip("Arraste apenas o holder que esse ring pertence")] public Transform correctHolder;

    [SerializeField] private float distanceToFit = 1f;

    [Header("Sprites Colorful")]
    [SerializeField] private Sprite normalColorful;
    [SerializeField] private Sprite lockedColorful;

    [Header("Sprites Black & White")]
    [SerializeField] private Sprite normalBlackAndWhite;
    [SerializeField] private Sprite lockedBlackAndWhite;
    [SerializeField] Image myImage;

    [Header("Áudio ao Clicar")]
    [SerializeField] private EventReference ringSound;

    [Header("Gerenciador do Puzzle")]
    public TonalLadderGerenciator puzzleGerence;

    private RectTransform myRect;
    private Vector2 ringInicialPos;
    private bool locked = false;
    private bool holdingRing = false;

    void Start()
    {
        if (RealityManager.Instance != null)
            RealityManager.Instance.onRealityChanged.AddListener(updateVisual);
        updateVisual();
        //tem que ser pelo RECT TRANSFORM, pra trabalhar com a posicao na UI
        myRect = GetComponent<RectTransform>();
        //guardar a posicao ancorada inicial do ring pra retornar (caso solte)
        ringInicialPos = myRect.anchoredPosition;
    }

    public void updateVisual()
    {
        bool isColorful = false;
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.Colorful)
            isColorful = true;

        if (locked)
        {
            if (isColorful)
                myImage.sprite = lockedColorful;
            else
                myImage.sprite = lockedBlackAndWhite;
        }
        else
        {
            if (isColorful)
                myImage.sprite = normalColorful;
            else
                myImage.sprite = normalBlackAndWhite;
        }
    }

    public void CatchRing()
    {
        //fix bug: se estivr segurando e trocar a realidade, essa funcao NAO deve rodar novamente
        if (holdingRing) return;
        holdingRing = true; //passa a "estar segurando" o disco

        if (RealityManager.Instance != null && RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            //AUDIO FMOD TOCAR
            AudioManager.Instance.PlayOneShot(ringSound, transform.position);
            Debug.Log("Toquei meu sonzinhooooo ding ding!");
        }
    }
    public void DragRing() //ativada enquanto SEGURA o ring
    {
        //tira de locked e atualiza a imagem
        locked = false;
        updateVisual();
        //pega a posicao (com o NOVO inputsystem)
        Vector2 mousePos = Mouse.current.position.ReadValue();
        //transforma em relacao a camera
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //pega o vetor3 da posicao do mouse e usa apenas x e y (mantem o z que ja tem)
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }

    public void DropRing() //ativada ao SOLTAR o ring
    {
        holdingRing = false;

        //Acha o holder mais proximo E que ainda esteja na distancia minima pra dar FIT
        Transform nearestHolder = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform holder in allHolders)
        {
            float distance = Vector2.Distance(transform.position, holder.position);
            if (distance < distanceToFit && distance < shortestDistance) //logica que expliquei no OBS
            {
                shortestDistance = distance;
                nearestHolder = holder;
            }
        }

        //Se achou onde "entraria", verifica se ja esta ocupado
        if (nearestHolder != null)
        {
            bool holderOcupped = false;
            foreach (RingDrag otherRing in puzzleGerence.allRings)
            {
                //calcula a distancia dos outros aneis ate esse holder (pra saber se ja tem algum aqui)
                float distance = Vector2.Distance(otherRing.transform.position, nearestHolder.position);
                if (otherRing != this && distance < 0.1f) //se tiver alguem la que NAO é esse anel
                {
                    holderOcupped = true; //avisa que esta ocupado e sai do loop
                    break;
                }
            }

            //se nao estiver ocupado, aloca la
            if (!holderOcupped)
            {
                transform.position = nearestHolder.position;
                locked = true;
            }
            else //se estiver ocupado, volta pra posicao inicial
            {
                myRect.anchoredPosition = ringInicialPos;
                locked = false;
            }
        }
        else //se a distancia for grande, volta ele pra posicao inicial
        {
            myRect.anchoredPosition = ringInicialPos;
            locked = false;
        }

        updateVisual(); //apos saber se esta preso ou nao, atualiza a imagem

        //SEMPRE que mover um puzzle, checa se ganhou (analisa a posicao de todos os aneis)
        if (puzzleGerence != null)
        {
            puzzleGerence.checkVictory();
        }
    }

    //verifica se o ring esta na posicao correta (bem proximo ao holder certo)
    public bool isOnCorrectHolder()
    {
        if (Vector2.Distance(transform.position, correctHolder.position) < 0.5f)
            return true;
        return false;
    }
}
