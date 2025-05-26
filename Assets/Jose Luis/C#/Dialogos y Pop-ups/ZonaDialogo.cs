using UnityEngine;
using System.Collections.Generic;

public class ZonaDialogo : MonoBehaviour
{
    public List<DialogoConAutor> mensajes = new List<DialogoConAutor>();

    [Header("Colisionador que se eliminará al terminar los diálogos (opcional)")]
    public GameObject colisionadorBloqueo;

    private bool yaActivado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (yaActivado) return;

        if (other.CompareTag("Player"))
        {
            JugadorDialogo jugador = other.GetComponent<JugadorDialogo>();
            if (jugador != null)
            {
                jugador.ActivarDialogos(mensajes, colisionadorBloqueo);
                yaActivado = true;
            }
        }
    }
}