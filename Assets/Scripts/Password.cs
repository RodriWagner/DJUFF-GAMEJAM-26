using UnityEngine;

public class Password : MonoBehaviour
{
    public string password;
    private string attempt = "";
    public void press()
    {
        if (TryGetComponent<ButtonCharacter>(out ButtonCharacter botao))
        {
            Debug.Log(botao.caracter);
            attempt = attempt + botao.caracter;
        }
    }
    public void enter()
    {
        if (string.Compare(attempt, password) == 0)
        {
            Debug.Log("acerto yayyyyyyyyyyyyyyyyyyyyyyy");
        }
        else
        {
            Debug.Log("burro AHHDAAWHSDGWA^%TDAWU");
        }
    }
    public void delete()
    {
        attempt = attempt.Remove(attempt.Length - 1);
    }
    public void clear()
    {
        attempt = "";
    }
}
