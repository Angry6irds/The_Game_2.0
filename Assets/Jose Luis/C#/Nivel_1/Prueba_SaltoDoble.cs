using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Profiling;

public class Prueba_SaltoDoble : MonoBehaviour
{
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    [SerializeField] private float radioBusqueda = 2f; // Rango para buscar al jugador
    private bool recogido = false;

    private void Update()
    {
        if (!recogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }
    }

    private void Recoger()
    {
        // Buscar al jugador dentro de un radio usando OverlapSphere este genera una esfera con Gizmos.DrawWireSphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        //Busca cualquier Collider dentro del radio.
        foreach (Collider colision in colliders)
        {
            if (colision.CompareTag("Player"))
            {
                PruebaSaltar scriptSalto = colision.GetComponent<PruebaSaltar>();
                if (scriptSalto != null)
                {
                    scriptSalto.ActivarDobleSalto();
                    recogido = true;

                    // Ocultar el objeto
                    MeshRenderer m = GetComponent<MeshRenderer>();
                    if (m != null) m.enabled = false;

                    Collider c = GetComponent<Collider>();
                    if (c != null) c.enabled = false;

                    Debug.Log("Doble salto activado con tecla E cerca del objeto.");
                    return;
                }
            }
        }
        Debug.Log("No hay jugador cerca para recoger el objeto.");
    }

    // Mostrar la esfera de busqueda en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}
