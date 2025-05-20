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

    private bool esperandoDialogo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!cristalDepositado && !esperandoDialogo && other.CompareTag("Player"))
        {
            InventarioJugador inventario = other.GetComponent<InventarioJugador>();
            if (inventario != null && inventario.tieneCristal)
            {
                // Marcar que está en proceso de diálogo
                esperandoDialogo = true;

                // Deposita el cristal desde el inventario
                inventario.tieneCristal = false;

                // Inicia los diálogos
                StartCoroutine(IniciarDialogoDeposito());

                Debug.Log("Cristal depositado. Iniciando diálogo.");
            }
        }
    }

    private IEnumerator IniciarDialogoDeposito()
    {
        // Reproducir los diálogos al depositar
        jugadorDialogo.ActivarDialogos(mensajesDeposito);

        // Esperar a que termine el panel de diálogo
        while (jugadorDialogo.dialogoUI.panelDialogo.activeSelf)
        {
            yield return null;
        }

        // Diálogo finalizado, permitir acceso
        cristalDepositado = true;
        dialogoFinalizado = true;
        esperandoDialogo = false;

        Debug.Log("Diálogo completado. La puerta ahora es accesible.");
    }
}