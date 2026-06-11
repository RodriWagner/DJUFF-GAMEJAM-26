using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public PlayerMoviment playerMoviment;
    public GameBGMusic bGMusic;
    private VideoPlayer cutscene;
    void Start()
    {
        cutscene = GetComponent<VideoPlayer>();
        cutscene.loopPointReached += endCutscene;
        playerMoviment.enabled = false;
    }

    void endCutscene(VideoPlayer vp)
    {
        gameObject.SetActive(false);
        bGMusic.FirstStartBGMusic();
        playerMoviment.enabled = true;
    }
}