using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BootScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text textoTerminal;
    [SerializeField] private Image barraCargaImage;
    [SerializeField] private Button botonPlay;

    [Header("Audio Retro")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoArranque;
    [SerializeField] private AudioClip sonidoBotonListo;

    [Header("Sprites de la Barra de Carga")]
    [SerializeField] private Sprite[] framesBarraCarga;

    [Header("Configuración de la Escena")]
    [SerializeField] private string nombreEscenaJuego = "SampleScene";
    [SerializeField] private float velocidadEscritura = 0.03f;

    [TextArea(5, 10)]
    [SerializeField] private string mensajeTerminal = 
        "C:\\POLICE_OS\\BOOT.EXE\n" +
        "MEMORIA DE SISTEMA: 640K OK\n" +
        "VERIFICANDO ARCHIVOS DEL CASO... OK\n" +
        "BASE DE DATOS CONECTADA.\n" +
        "AGENTE AUTENTICADO.\n\n" +
        "SISTEMA LISTO.";

    private void Start()
    {
        if (botonPlay != null)
        {
            botonPlay.interactable = false;
            botonPlay.onClick.AddListener(CargarJuego);
        }

        if (barraCargaImage != null && framesBarraCarga.Length > 0)
        {
            barraCargaImage.sprite = framesBarraCarga[0];
        }

        if (audioSource != null && sonidoArranque != null)
        {
            audioSource.clip = sonidoArranque;
            audioSource.loop = true;
            audioSource.Play();
        }

        StartCoroutine(EfectoTipeoYCarga());
    }

    private IEnumerator EfectoTipeoYCarga()
    {
        textoTerminal.text = "";
        int totalCaracteres = mensajeTerminal.Length;
        int caracteresActuales = 0;

        foreach (char letra in mensajeTerminal.ToCharArray())
        {
            textoTerminal.text += letra;
            caracteresActuales++;

            if (barraCargaImage != null && framesBarraCarga.Length > 0)
            {
                float porcentaje = (float)caracteresActuales / totalCaracteres;
                int frameIndex = Mathf.Clamp(Mathf.FloorToInt(porcentaje * framesBarraCarga.Length), 0, framesBarraCarga.Length - 1);
                barraCargaImage.sprite = framesBarraCarga[frameIndex];
            }

            yield return new WaitForSeconds(velocidadEscritura);
        }

        // Habilitar el botón y lanzar el beep encima del sonido de fondo
        if (botonPlay != null)
        {
            botonPlay.interactable = true;
            
            if (audioSource != null && sonidoBotonListo != null)
            {
                audioSource.PlayOneShot(sonidoBotonListo);
            }
        }
    }

    public void CargarJuego()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}