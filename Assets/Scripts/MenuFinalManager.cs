using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinalManager : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [SerializeField] private string nombreEscenaInicio = "PantallaInicio";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip musicaFinal;

    private void Start()
    {
        if (audioSource != null && musicaFinal != null)
        {
            audioSource.clip = musicaFinal;
            audioSource.loop = false; // Se reproduce una sola vez
            audioSource.Play();
        }
    }

    public void VolverAlInicio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        SceneManager.LoadScene(nombreEscenaInicio);
    }
}