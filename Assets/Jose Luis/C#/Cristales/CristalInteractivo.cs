using UnityEngine;

public class CristalInteractivo : MonoBehaviour
{
    public Transform puntoDeAgarreJugador;
    private bool estaTomado = false;

    private void Update()
    {
        if (estaTomado)
        {
            transform.position = puntoDeAgarreJugador.position;
            transform.rotation = puntoDeAgarreJugador.rotation;
        }
    }

    public void Tomar(Transform agarre)
    {
        estaTomado = true;
        puntoDeAgarreJugador = agarre;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }
}