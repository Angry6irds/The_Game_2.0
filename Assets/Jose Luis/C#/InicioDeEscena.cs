using UnityEngine;

public class InicioDeEscena : MonoBehaviour
{
    private void Start()
    {
        // Busca el controlador de transición (el que tiene la imagen negra)
        TransicionEscena transicion = FindObjectOfType<TransicionEscena>();
        if (transicion != null)
        {
            StartCoroutine(transicion.FadeDesdeNegro());
        }
        else
        {
            Debug.LogWarning("No se encontró el script TransicionEscena en la escena.");
        }
    }
}