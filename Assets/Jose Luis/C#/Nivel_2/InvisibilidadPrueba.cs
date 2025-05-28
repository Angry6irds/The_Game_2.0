using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Invisibilidad : MonoBehaviour
{
    [Header("Recoleccion del cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Activacion de invisibilidad")]
    [SerializeField] private KeyCode teclaInvisibilidad = KeyCode.H;
    private bool enTemporizador = false;

    [Header("Rango de recogida")]
    [SerializeField] private float radioBusqueda = 2f;

    [Header("Duracion de invisibilidad")]
    [SerializeField] private float duracionInvisibilidad = 5f;

    private GameObject jugador;

    void Update()
    {
        // Recojer el cristal si no se ha recogido
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        // Activar invisibilidad si ya fue recogido
        if (objetoRecogido && !enTemporizador && Input.GetKeyDown(teclaInvisibilidad))
        {
            StartCoroutine(TemporizadorInvisibilidad());
        }
    }

    void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider colision in colliders)
        {
            if (colision.CompareTag("Player"))
            {
                jugador = colision.gameObject;

                objetoRecogido = true;

                // Ocultar visualmente el cristal
                MeshRenderer mesh = GetComponent<MeshRenderer>();
                if (mesh != null) mesh.enabled = false;

                Collider tocar = GetComponent<Collider>();
                if (tocar != null) tocar.enabled = false;

                Debug.Log("Cristal de invisibilidad recogido.");
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger el cristal.");
    }

    IEnumerator TemporizadorInvisibilidad()
    {
        enTemporizador = true;

        if (jugador != null)
        {
            // Cambiar el tag para que los enemigos no detecten al jugador
            string tagOriginal = jugador.tag;
            jugador.tag = "Invisible"; // Cambia a un tag temporal no detectado

            // Cambiar visual del jugador (opcional, puedes usar material transparente)
            Renderer[] renders = jugador.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renders)
            {
                r.enabled = false; // Ocultar visualmente al jugador
            }

            Debug.Log("Jugador invisible.");

            yield return new WaitForSeconds(duracionInvisibilidad);

            // Restaurar
            jugador.tag = tagOriginal;

            foreach (Renderer r in renders)
            {
                r.enabled = true; // Mostrar de nuevo al jugador
            }

            Debug.Log("Invisibilidad terminada.");
        }

        enTemporizador = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}