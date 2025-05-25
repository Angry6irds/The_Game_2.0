using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscena : MonoBehaviour
{
    [Header("Nombre de la siguiente escena")]
    public string nombreDeEscena;

    [Header("¿Requiere cristal para pasar?")]
    public bool requiereCristal = false;

    [Header("Referencia a la caja de depósito (solo si se requiere cristal)")]
    public DepositBox cajaDeposito;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!requiereCristal)
        {
            TransicionEscena transicion = FindObjectOfType<TransicionEscena>();
            if (transicion != null)
            {
                transicion.IniciarTransicion(nombreDeEscena);
                return;
            }
            // Escena libre, sin restricciones
            SceneManager.LoadScene(nombreDeEscena);
        }
        else if (cajaDeposito != null && cajaDeposito.cristalDepositado && cajaDeposito.dialogoFinalizado)
        {
            TransicionEscena transicion = FindObjectOfType<TransicionEscena>();
            if (transicion != null)
            {
                transicion.IniciarTransicion(nombreDeEscena);
                return; // ¡Esto es vital!
            }
            else
            {
                SceneManager.LoadScene(nombreDeEscena); // fallback
            }
        }
        else
        {
            Debug.Log("Esta puerta requiere que deposites el cristal y que el diálogo termine.");
        }

    }

}