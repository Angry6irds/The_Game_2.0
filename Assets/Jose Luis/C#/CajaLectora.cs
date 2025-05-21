using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBox : MonoBehaviour
{
    [Header("Referencia al cristal blanco")]
    public string tagCristal = "CristalBlanco";

    [Header("Estado de depósito")]
    public bool cristalDepositado = false;
    [HideInInspector] public bool dialogoFinalizado = false;

    [Header("Punto de colocación del cristal")]
    public Transform puntoDeposito;

    [Header("Sistema de diálogo")]
    public JugadorDialogo jugadorDialogo;
    public List<DialogoConAutor> mensajesDeposito;

    [Header("Diálogo del jugador después del video")]
    public List<DialogoConAutor> mensajesDelJugador;

    [Header("Cinemática de video")]
    public CinematicaControl cinematicaControl;

    private bool esperandoDialogo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!cristalDepositado && !esperandoDialogo && other.CompareTag("Player"))
        {
            InventarioJugador inventario = other.GetComponent<InventarioJugador>();
            if (inventario != null && inventario.tieneCristal)
            {
                esperandoDialogo = true;

                inventario.tieneCristal = false;

                // Inicia la cinemática del video y diálogos
                StartCoroutine(EsperarCinematica());

                Debug.Log("Cristal depositado. Iniciando cinemática y diálogo.");
            }
        }
    }

    private IEnumerator EsperarCinematica()
    {
        // Inicia la secuencia de video y diálogos
        cinematicaControl.IniciarCinematica(mensajesDeposito, mensajesDelJugador);

        // Esperar hasta que finalice la cinemática completa
        while (!cinematicaControl.CinematicaTerminada)
        {
            yield return null;
        }

        // Ahora sí marcar todo como terminado
        cristalDepositado = true;
        dialogoFinalizado = true;
        esperandoDialogo = false;

        Debug.Log("Cinemática completada. La puerta ahora es accesible.");
    }
}