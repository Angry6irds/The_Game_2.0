using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerCristalesBlancos : MonoBehaviour
{
    public Transform puntoDeAgarre;
    private CristalInteractivo cristalActual;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Revisa si hay un cristal cerca
            if (cristalActual != null)
            {
                cristalActual.Tomar(puntoDeAgarre);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CristalBlanco"))
        {
            cristalActual = other.GetComponent<CristalInteractivo>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CristalBlanco"))
        {
            if (other.GetComponent<CristalInteractivo>() == cristalActual)
            {
                cristalActual = null;
            }
        }
    }
}
