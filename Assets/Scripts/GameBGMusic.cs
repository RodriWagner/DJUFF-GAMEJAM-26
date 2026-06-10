using FMODUnity;
using UnityEngine;

public class GameBGMusic : MonoBehaviour
{
    public EventReference musicBlackandWhite;
    public EventReference musicColored;

    void Start()
    {
        if (RealityManager.Instance != null) //aloca no invoke do onRealityChanged
            RealityManager.Instance.onRealityChanged.AddListener(ChangeMusic);
        AudioManager.Instance.StartMusic(musicBlackandWhite);
    }

    public void ChangeMusic()
    {
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            StopBGMusic();
            AudioManager.Instance.StartMusic(musicBlackandWhite);
        }
        else
        {
            StopBGMusic();
            AudioManager.Instance.StartMusic(musicColored);
        }
    }

    public void StopBGMusic()
    {
        AudioManager.Instance.StopMusic();
    }
}
