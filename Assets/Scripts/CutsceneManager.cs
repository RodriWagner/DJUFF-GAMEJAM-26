using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer meuVideoPlayer;

    [Header("O que esconder durante o vídeo?")]

    private bool videoJaComecou = false; 
    public bool Starter;

    void Awake()
    {
        meuVideoPlayer = GetComponent<VideoPlayer>();
    }

    // Essa função roda sozinha quando a Unity termina de carregar o MP4 (leva uns milissegundos)
    public void DarOPlayDeVerdade(VideoPlayer vp)
    {
        vp.Play();
    }

    void Update()
    {
        // Só monitora se o vídeo já estiver preparado e tocando
        if (!videoJaComecou && meuVideoPlayer.isPlaying)
        {
            videoJaComecou = true;
        }

        // Se o vídeo estava tocando e agora parou, é porque chegou no fim!
        if (videoJaComecou && !meuVideoPlayer.isPlaying)
        {
            if (!Starter) FimDaCutscene();
            else NextScene();
            this.enabled = false; // Desliga o script para parar o Update
        }
    }

    private void FimDaCutscene()
    {
        // 3. Devolve o jogo!
        Application.Quit();
    }
    private void NextScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}