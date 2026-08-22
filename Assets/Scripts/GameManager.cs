using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void IniciarInvestigacion()
    {
        panelInicio.SetActive(false);

        panelPrincipal.SetActive(true);
    }

    public void MostrarCamaras()
    {
        textoBienvenida.SetActive(false);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();

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
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();

        vioHuellas = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nInforme_Huellas.pdf\r\n\r\n--------------------------------------\r\n\r\nResultado:\r\n\r\nLas huellas encontradas pertenecen a un ex empleado del banco.\r\n\r\nSegún el registro de personal, únicamente Carlos Pérez trabajó anteriormente en la entidad.";
        ActualizarProgreso();

        AgregarNota("Las huellas pertenecen a un ex empleado del banco.");

        imagenEvidencia.sprite = imagenHuellas;
    }

    public void MostrarMensajes()
    {
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();

        vioMensajes = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nChat_Recuperado.txt\r\n\r\n--------------------------------------\r\n\r\n21:03\r\n\r\n— No olvides llevar la chaqueta negra y la llave.\r\n\r\n21:05\r\n\r\n— Tranquilo, todavía conservo la llave que nunca devolví.\r\n\r\nEl nombre del remitente fue eliminado.";
        ActualizarProgreso();

        AgregarNota("Uno de los involucrados aún conservaba una llave del banco.");

        imagenEvidencia.sprite = imagenMensajes;
    }

    public void MostrarBanco()
    {
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();

        vioBanco = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nMovimientos_Bancarios.xlsx\r\n\r\n--------------------------------------\r\n\r\nTransferencia:\r\n\r\nDestino:\r\n\r\nAndrés Ruiz\r\n\r\nValor:\r\n\r\n$5.000.000\r\n\r\nObservación:\r\n\r\nPago recibido menos de 24 horas después del robo.";
        ActualizarProgreso();

        AgregarNota("Andrés recibió dinero después del robo.");

        imagenEvidencia.sprite = imagenBanco;
    }

    public void MostrarLlamadas()
    {
        textoBienvenida.SetActive(false);
        textoInformacion.gameObject.SetActive(true);
        imagenEvidencia.gameObject.SetActive(true);
        OcultarBotones();

        vioLlamadas = true;
        textoInformacion.text =
            "Archivo abierto:\r\n\r\nRegistro_Llamadas.csv\r\n\r\n--------------------------------------\r\n\r\n21:11\r\n\r\nLlamada entre Carlos Pérez y Andrés Ruiz.\r\n\r\nDuración:\r\n\r\n03:12 minutos.\r\n\r\nNo fue posible recuperar el contenido.";
        ActualizarProgreso();

        AgregarNota("Carlos habló con Andrés minutos antes del robo.");

        imagenEvidencia.sprite = imagenLlamadas;
    }

    public void Acusar()
    {
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
        OcultarPantallas();
        OcultarBotones();

        textoFinal.SetActive(true);

        contenidoTextoFinal.text =
                        "¡CASO RESUELTO!\r\n\r\nLas cámaras muestran a una persona con chaqueta negra.\r\n\r\nLos mensajes hacen referencia a esa misma chaqueta y a una reunión cerca del banco.\r\n\r\nEl registro de llamadas confirma comunicación entre los involucrados antes del robo.\r\n\r\nLas huellas en la bóveda demuestran que Carlos estuvo en contacto con la escena del crimen.\r\n\r\nLa evidencia reunida permite concluir que Carlos es el responsable del robo.";

    }

    public void ElegirLaura()
    {
        OcultarPantallas();
        OcultarBotones();

        textoFinal.SetActive(true);

        contenidoTextoFinal.text =
                        "CASO FALLIDO\r\n\r\nLaura aparece en los mensajes, pero no existe ninguna evidencia que la ubique dentro del banco.\r\n\r\nLas huellas y el video no permiten relacionarla directamente con el robo.\r\n\r\nNo hay pruebas suficientes para considerarla culpable.";

    }

    public void ElegirAndres()
    {
        OcultarPantallas();
        OcultarBotones();

        textoFinal.SetActive(true);

        contenidoTextoFinal.text =
                        "CASO FALLIDO\r\n\r\nAunque Andrés recibió dinero después del robo, ninguna evidencia demuestra que haya ingresado al banco.\r\n\r\nEl movimiento bancario por sí solo no es suficiente para probar su participación.\r\n\r\nLa investigación debe continuar.";

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
            notas += "• " + nota + "\n\n";
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
        OcultarPantallas();
        OcultarBotones();

        textoExpediente.SetActive(true);

        contenidoTextoExpediente.text =
        "Archivo abierto:\n\n" +
        "Expediente_Sospechosos.pdf\n" +
        "--------------------------------------\n" +
        "EXPEDIENTE DE SOSPECHOSOS\n" +

        "Carlos Pérez\n" +
        "• Ex empleado del banco.\n" +
        "• Renunció hace 6 meses.\n" +
        "• Conservaba una copia de la llave maestra del banco.\n" +

        "--------------------------------------\n" +

        "Laura Gómez\n" +
        "• Cliente frecuente.\n" +
        "• Visitó el banco la mañana del robo.\n" +
        "• No posee antecedentes penales.\n" +

        "--------------------------------------\n" +

        "Andrés Ruiz\n" +
        "• Técnico de seguridad.\n" +
        "• Responsable del mantenimiento de las cámaras.\n" +
        "• Tenía acceso al sistema de vigilancia.";
    }
}

