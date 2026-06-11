using UnityEngine;

public class RealityMenu : MonoBehaviour
{
    public Animator anim;
    public void Start()
    {
        if (RealityManager.Instance != null) //aloca no invoke do onRealityChanged
            RealityManager.Instance.onRealityChanged.AddListener(AnimateChange);
    }
    public void AnimateChange()
    {
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            anim.SetTrigger("ChangeC");
        }
        else
        {
            anim.SetTrigger("ChangeBW");
        }
    }
}
