using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private float lastValue = 1;
    private enum SliderTypes {GERAL, MUSICA, EFEITOS};
    [SerializeField] private SliderTypes sliderType;

    [SerializeField] private EventReference geralTestSound;
    [SerializeField] private EventReference effectsTestSound;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value != lastValue)
        {
            changeVolume();
            lastValue = slider.value;
        }
    }

    private void changeVolume()
    {
        switch (sliderType)
        {
            case (SliderTypes.GERAL):
                AudioManager.Instance.SetGeneralVolume(slider.value);
                AudioManager.Instance.PlayOneShot(geralTestSound, transform.position);
                break;
            case (SliderTypes.MUSICA):
                AudioManager.Instance.SetMusicVolume(slider.value);
                break;
            case (SliderTypes.EFEITOS):
                AudioManager.Instance.SetEffectsVolume(slider.value);
                AudioManager.Instance.PlayOneShot(effectsTestSound, transform.position);
                break;
        }
    }
}
