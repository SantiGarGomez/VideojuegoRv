using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text textoInformacion;
    public GameObject textoAcusar;
    public GameObject botonCarlos;
    public GameObject botonLaura;
    public GameObject botonAndres;
    public GameObject panelInicio;
    public GameObject panelPrincipal;
    public TMP_Text textoProgreso;
    public TMP_Text textoNotas;
    private string notas = "";
    public GameObject textoFinal;
    public TMP_Text contenidoTextoFinal;
    public GameObject textoExpediente;
    public TMP_Text contenidoTextoExpediente;

    public Image imagenEvidencia;
    public GameObject textoBienvenida;

    public Sprite imagenCamaras;
    public Sprite imagenHuellas;
    public Sprite imagenMensajes;
    public Sprite imagenBanco;
    public Sprite imagenLlamadas;

    [Header("Escenas de Final")]
    [SerializeField] private string nombreEscenaFinalBueno = "FinalBueno";
    [SerializeField] private string nombreEscenaFinalMalo = "FinalMalo";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoFondoRetro; // Ruido de fondo/computador retro
    [SerializeField] private AudioClip sonidoClic;      // Sonido corto al presionar botones

    [Header("UI Mute Button")]
    [SerializeField] private Image imagenBotonMute;      // Componente Image del botón de Mute
    [SerializeField] private Sprite spriteAudioOn;       // Icono de audio activado
    [SerializeField] private Sprite spriteAudioOff;      // Icono de audio silenciado
    [SerializeField] private TMP_Text textoBotonMute;    // Opcional: si usas texto en lugar de sprite (ej. "AUDIO: ON" / "AUDIO: OFF")

    private bool estaSilenciado = false;

    private bool vioCamaras = false;
    private bool vioHuellas = false;
    private bool vioMensajes = false;
    private bool vioBanco = false;
    private bool vioLlamadas = false;

    void Start()
    {
        ActualizarProgreso();
        textoBienvenida.SetActive(true);
        textoInformacion.gameObject.SetActive(false);
        textoAcusar.SetActive(false);
        textoFinal.SetActive(false);
        imagenEvidencia.gameObject.SetActive(false);
        textoExpediente.SetActive(false);

        // Iniciar el sonido de fondo continuo
        if (audioSource != null && sonidoFondoRetro != null)
        {
            audioSource.clip = sonidoFondoRetro;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Asegurarse de que el audio no empiece silenciado
        AudioListener.pause = false;
        ActualizarUIAudio();
    }

    // Método para alternar el silencio total del juego
    public void ToggleAudio()
    {
        estaSilenciado = !estaSilenciado;
        AudioListener.pause = estaSilenciado;

        if (!estaSilenciado)
        {
            ReproducirClic();
        }

        ActualizarUIAudio();
    }

    private void ActualizarUIAudio()
    {
        // Si usas sprites para el icono del botón
        if (imagenBotonMute != null)
        {
            if (estaSilenciado && spriteAudioOff != null)
                imagenBotonMute.sprite = spriteAudioOff;
            else if (!estaSilenciado && spriteAudioOn != null)
                imagenBotonMute.sprite = spriteAudioOn;
        }

        // Si usas texto en el botón
        if (textoBotonMute != null)
        {
            textoBotonMute.text = estaSilenciado ? "AUDIO: OFF" : "AUDIO: ON";
        }
    }

    // Método para reproducir el clic en los botones
    private void ReproducirClic()
    {
        if (audioSource != null && sonidoClic != null && !estaSilenciado)
        {
            audioSource.PlayOneShot(sonidoClic);
        }
    }

    public void IniciarInvestigacion()
    {
        ReproducirClic();
        panelInicio.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void MostrarCamaras()
    {
        ReproducirClic();
        textoBienvenida.SetActive(false);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();
        textoExpediente.SetActive(false);

        vioCamaras = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nCamara_Exterior_01.mp4\r\n\r\n--------------------------------------\r\n\r\nHora: 21:15\r\n\r\nDescripción:\r\n\r\nLa cámara registra a una persona con chaqueta negra entrando al banco.\r\n\r\nEl sospechoso cojea ligeramente de la pierna izquierda.\r\n\r\nEl rostro no puede identificarse.";
        ActualizarProgreso();

        AgregarNota("El sospechoso tenía una leve cojera y vestía una chaqueta negra.");
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.sprite = imagenCamaras;
    }

    public void MostrarHuellas()
    {
        ReproducirClic();
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();
        textoExpediente.SetActive(false);

        vioHuellas = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nInforme_Huellas.pdf\r\n\r\n--------------------------------------\r\n\r\nResultado:\r\n\r\nLas huellas encontradas pertenecen a un ex empleado del banco.\r\n\r\nSegún el registro de personal, únicamente Carlos Pérez trabajó anteriormente en la entidad.";
        ActualizarProgreso();

        AgregarNota("Las huellas pertenecen a un ex empleado del banco.");
        imagenEvidencia.sprite = imagenHuellas;
    }

    public void MostrarMensajes()
    {
        ReproducirClic();
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();
        textoExpediente.SetActive(false);

        vioMensajes = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nChat_Recuperado.txt\r\n\r\n--------------------------------------\r\n\r\n21:03\r\n\r\n- No olvides llevar la chaqueta negra y la llave.\r\n\r\n21:05\r\n\r\n- Tranquilo, todavía conservo la llave que nunca devolví.\r\n\r\nEl nombre del remitente fue eliminado.";
        ActualizarProgreso();

        AgregarNota("Uno de los involucrados aún conservaba una llave del banco.");
        imagenEvidencia.sprite = imagenMensajes;
    }

    public void MostrarBanco()
    {
        ReproducirClic();
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();
        textoExpediente.SetActive(false);

        vioBanco = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nMovimientos_Bancarios.xlsx\r\n\r\n--------------------------------------\r\n\r\nTransferencia:\r\n\r\nDestino:\r\n\r\nAndrés Ruiz\r\n\r\nValor:\r\n\r\n$5.000.000\r\n\r\nObservación:\r\n\r\nPago recibido menos de 24 horas después del robo.";
        ActualizarProgreso();

        AgregarNota("Andrés recibió dinero después del robo.");
        imagenEvidencia.sprite = imagenBanco;
    }

    public void MostrarLlamadas()
    {
        ReproducirClic();
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();
        textoExpediente.SetActive(false);

        vioLlamadas = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nRegistro_Llamadas.csv\r\n\r\n--------------------------------------\r\n\r\n21:11\r\n\r\nLlamada entre Carlos Pérez y Andrés Ruiz.\r\n\r\nDuración:\r\n\r\n03:12 minutos.\r\n\r\nNo fue posible recuperar el contenido.";
        ActualizarProgreso();

        AgregarNota("Carlos habló con Andrés minutos antes del robo.");
        imagenEvidencia.sprite = imagenLlamadas;
    }

    public void Acusar()
    {
        ReproducirClic();
        if (vioCamaras && vioHuellas && vioMensajes && vioBanco && vioLlamadas)
        {
            OcultarPantallas();
            textoAcusar.SetActive(true);
            botonCarlos.SetActive(true);
            botonLaura.SetActive(true);
            botonAndres.SetActive(true);
        }
        else
        {
            textoInformacion.text =
                "Debes revisar todas las evidencias.\n\n" +
                "Cámaras: " + (vioCamaras ? "realizado" : "pendiente") + "\n" +
                "Huellas: " + (vioHuellas ? "realizado" : "pendiente") + "\n" +
                "Mensajes: " + (vioMensajes ? "realizado" : "pendiente") + "\n" +
                "Banco: " + (vioBanco ? "realizado" : "pendiente") + "\n" +
                "Llamadas: " + (vioLlamadas ? "realizado" : "pendiente");
        }
    }

    public void ElegirCarlos()
    {
        ReproducirClic();
        SceneManager.LoadScene(nombreEscenaFinalBueno);
    }

    public void ElegirLaura()
    {
        ReproducirClic();
        SceneManager.LoadScene(nombreEscenaFinalMalo);
    }

    public void ElegirAndres()
    {
        ReproducirClic();
        SceneManager.LoadScene(nombreEscenaFinalMalo);
    }

    void OcultarBotones()
    {
        botonCarlos.SetActive(false);
        botonLaura.SetActive(false);
        botonAndres.SetActive(false);
        textoAcusar.SetActive(false);
    }

    void ActualizarProgreso()
    {
        int evidencias = 0;
        if (vioCamaras) evidencias++;
        if (vioHuellas) evidencias++;
        if (vioMensajes) evidencias++;
        if (vioBanco) evidencias++;
        if (vioLlamadas) evidencias++;

        textoProgreso.text = "Progreso: " + evidencias + " / 5 evidencias";
    }

    void AgregarNota(string nota)
    {
        if (!notas.Contains(nota))
        {
            notas += "- " + nota + "\n\n";
            textoNotas.text = notas;
        }
    }

    void OcultarPantallas()
    {
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(false);
        textoAcusar.SetActive(false);
        textoFinal.SetActive(false);
        imagenEvidencia.gameObject.SetActive(false);
        textoExpediente.SetActive(false);
    }

    public void MostrarExpediente()
    {
        ReproducirClic();
        OcultarPantallas();
        OcultarBotones();
        textoExpediente.SetActive(true);

        contenidoTextoExpediente.text =
        "Archivo abierto:\n\n" +
        "Expediente_Sospechosos.pdf\n" +
        "--------------------------------------\n" +
        "EXPEDIENTE DE SOSPECHOSOS\n" +
        "Carlos Pérez\n" +
        "- Ex empleado del banco.\n" +
        "- Renunció hace 6 meses.\n" +
        "- Conservaba una copia de la llave maestra del banco.\n" +
        "--------------------------------------\n" +
        "Laura Gómez\n" +
        "- Cliente frecuente.\n" +
        "- Visitó el banco la mañana del robo.\n" +
        "- No posee antecedentes penales.\n" +
        "--------------------------------------\n" +
        "Andrés Ruiz\n" +
        "- Técnico de seguridad.\n" +
        "- Responsable del mantenimiento de las cámaras.\n" +
        "- Tenía acceso al sistema de vigilancia.";
    }
}