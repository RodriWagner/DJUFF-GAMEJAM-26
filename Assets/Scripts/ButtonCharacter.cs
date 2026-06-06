using UnityEngine;
using TMPro;

public class ButtonCharacter : MonoBehaviour
{
    public TMP_Text buttonText;
    public string caracter;
    void Update()
    {
        buttonText.text = caracter;
    }
}
