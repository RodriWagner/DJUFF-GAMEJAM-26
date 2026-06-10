using System.ComponentModel;
using FMODUnity;
using UnityEngine;

public class BookSounds : MonoBehaviour
{
    [Header("Sons a serem tocados")]
    [Tooltip("Som do livro 1")] public EventReference One;
    [Tooltip("Som do livro 2")] public EventReference Two;
    [Tooltip("Som do livro 3")] public EventReference Three;
    [Tooltip("Som do livro 4")] public EventReference Four;
    [Tooltip("Som do livro 5")] public EventReference Five;
    public void Sound1()
    {
        AudioManager.Instance.PlayOneShot(One, transform.position);
    }
    public void Sound2()
    {
        AudioManager.Instance.PlayOneShot(Two, transform.position);
    }
    public void Sound3()
    {
        AudioManager.Instance.PlayOneShot(Three, transform.position);
    }
    public void Sound4()
    {
        AudioManager.Instance.PlayOneShot(Four, transform.position);
    }
    public void Sound5()
    {
        AudioManager.Instance.PlayOneShot(Five, transform.position);
    }
}
