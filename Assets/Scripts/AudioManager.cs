using System.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    // isso é pras musicas que loopam (como as de fundo):
    private EventInstance musicInstance;

    // Bus para regular os volumes
    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void SetGeneralVolume(float volume)
    {
        masterBus.setVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicBus.setVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        sfxBus.setVolume(volume);
    }

    public void PlayOneShot(EventReference soundEvent, Vector3 soundPosition)
    {
        RuntimeManager.PlayOneShot(soundEvent, soundPosition);
    }

    public void StartMusic(EventReference musicEvent)
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    public void StopMusic()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release(); // isso  libera a memoria
        }
    }
}
