using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traspasar : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField] private string ObjetoTraspasable = "Objeto_Traspasable";

    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Activar/desactivar pared")]
    [SerializeField] private KeyCode ActivarCristal = KeyCode.F;
    private bool enTemporizador = false;

    [Header("Rango adquirido")]
    [SerializeField] private float radioBusqueda = 2f;
    private bool recogido = false;

    void Update()
    {
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        if (Input.GetKeyDown(ActivarCristal) && !enTemporizador)
        {
            StartCoroutine(TemporizadorTraspaso());
        }

    }

    void Recoger()
    {
        // Buscar si el jugador está cerca del objeto
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                objetoRecogido = true;
                Debug.Log("Cristal recogido.");

                // Ocultar el objeto visualmente
                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                return;
            }
            Debug.Log("No hay jugador cerca para recoger el cristal.");
        }
    }

    IEnumerator TemporizadorTraspaso()
    {
        enTemporizador = true; // Bloquear para evitar multiples activaciones y el jugador no haga spam

        // Buscar el objeto con el tag "Objeto_Traspasable"
        GameObject[] objetosTraspasables = GameObject.FindGameObjectsWithTag(ObjetoTraspasable);


        /* Para Ale: objetosTraspasables es un arreglo de GameObjects. Este arreglo contiene
         todos los objetos en la escena que tienen el tag "Objeto_Traspasable.
         Lo que hace foreach: es  recorrer todos los elementos de un arreglo uno por uno.
         */

        if (objetosTraspasables.Length > 0)
        {
            foreach (GameObject obj in objetosTraspasables)
            {
                Collider colision = obj.GetComponent<Collider>();
                if (colision != null)
                {
                    colision.isTrigger = true;
                }
            }

            Debug.Log("Todos los objetos traspasables se pueden atravesar por 5 segundos.");
            yield return new WaitForSeconds(5f);

            //El segundo foreach es para deactivar el trigger

            foreach (GameObject obj in objetosTraspasables)
            {
                Collider colision = obj.GetComponent<Collider>();
                if (colision != null)
                {
                    colision.isTrigger = false;
                }
            }

            Debug.Log("Ya no se pueden traspasar los objetos.");
        }
        else
        {
            Debug.LogError("No se encontraron objetos con el tag '" + ObjetoTraspasable + "'.");
        }

        enTemporizador = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}

