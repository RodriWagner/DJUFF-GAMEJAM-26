using System.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class RealityAudio : MonoBehaviour
{
    public static RealityAudio Instance;
    [Header("Sons de Fundo")]
    public EventReference backMenu;
    public EventReference backBlackAndWhite;
    public EventReference backColorful;

    private EventInstance instanceMenu;
    private EventInstance instanceBlackAndWhite;
    private EventInstance instanceColorful;

    void Awake()
    {
        //instanciar essa classe
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //instancia todos os eventos
        initiateMusic(instanceMenu, backMenu, 1.0f);
        initiateMusic(instanceBlackAndWhite, backBlackAndWhite, 0f);
        initiateMusic(instanceColorful, backColorful, 0f);
    }

    void Start()
    {
        if (RealityManager.Instance != null) //aloca no invoke do onRealityChanged
            RealityManager.Instance.onRealityChanged.AddListener(ChangeMusic);
    }
    public void ChangeMusic()
    {
        if (RealityManager.Instance.currentReality == RealityManager.RealityType.BlackAndWhite)
        {
            instanceBlackAndWhite.setVolume(1.0f);
            instanceColorful.setVolume(0f);
        }
        else
        {
            instanceBlackAndWhite.setVolume(0f);
            instanceColorful.setVolume(1.0f);
        }
    }

    public void initiateOnGameMusic()
    {
        instanceMenu.setVolume(0f);
        ChangeMusic();
    }
    //modularizacao so pra organizar a iniciacao do AWAKE
    void initiateMusic(EventInstance musicInstance, EventReference musicRef, float volume)
    {
        musicInstance = RuntimeManager.CreateInstance(musicRef);
        musicInstance.setVolume(volume);
        musicInstance.start();
    }
}