using FMODUnity;
using UnityEngine;

public class MenuBGMusic : MonoBehaviour
{
    public EventReference music;

    void Start()
    {
        AudioManager.Instance.StartMusic(music);
    }
}
