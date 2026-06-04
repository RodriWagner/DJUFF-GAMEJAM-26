using TMPro;
using UnityEngine;

public class RealitySprites : MonoBehaviour
{
    //OBJETOS QUE EXISTEM EM AMBAS AS REALIDADES, MAS SOMENTE O SPRITE MUDA NELES
    [Header("Sprite das Realidades")]
    public Sprite spriteBlackAndWhite;
    public Sprite spriteColorful;
    private SpriteRenderer myRenderer;
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        if (RealityManager.Instance != null)
        {
            RealityManager.Instance.onRealityChanged.AddListener(Updatesprite);
        }
        Updatesprite();
    }
    void Updatesprite()
    {
        if (myRenderer != null)
        {
            if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
                myRenderer.sprite = spriteBlackAndWhite;
            else
                myRenderer.sprite = spriteColorful;
        }
    }
}
