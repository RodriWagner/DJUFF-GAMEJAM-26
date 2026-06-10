using UnityEngine;

public class FinalDrawerOpen : MonoBehaviour
{
    public GameObject HanoiUI;
    public void OnEnable()
    {
        HanoiUI.SetActive(false);
    }
}
