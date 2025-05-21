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
            // Escena libre, sin restricciones
            SceneManager.LoadScene(nombreDeEscena);
        }
        else if (cajaDeposito != null && cajaDeposito.cristalDepositado && cajaDeposito.dialogoFinalizado)
        {
            // Escena con cristal correctamente depositado Y diálogo terminado
            SceneManager.LoadScene(nombreDeEscena);
        }
        else
        {
            Debug.Log("Esta puerta requiere que deposites el cristal y que el diálogo termine.");
        }
    }
}