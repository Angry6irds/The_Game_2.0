using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentoVelocidad : MonoBehaviour
{
    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Rango adquirido")]
    [SerializeField] private float radioBusqueda = 2f;

    private GameObject jugador;
    private Player_Standar movimientoJugador;

    void Update()
    {
        // Recojer el cristal si no se ha recogido
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        // Aumentar velocidad solo si se recogió el cristal y el jugador tiene el componente
        if (objetoRecogido && jugador != null && movimientoJugador != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movimientoJugador.velActual = movimientoJugador.velCaminata * 4f; // correr
            }
            else
            {
                movimientoJugador.velActual = movimientoJugador.velCaminata; // caminar normal
            }
        }
    }

    public void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                jugador = col.gameObject;

                // Obtener el componente de movimiento
                movimientoJugador = jugador.GetComponent<Player_Standar>();

                if (movimientoJugador == null)
                {
                    Debug.LogError("El jugador no tiene el componente 'Player_Standar'.");
                    return;
                }

                objetoRecogido = true;

                // Ocultar este objeto visualmente
                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                Debug.Log("Cristal de velocidad recogido.");
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger el cristal.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}