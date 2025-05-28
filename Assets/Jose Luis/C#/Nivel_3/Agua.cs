using UnityEngine;

public class Agua : MonoBehaviour
{
    [Header("Nueva gravedad para el jugador")]
    [SerializeField] private float nuevaGravedad = -2f;

    [Header("Tag del jugador")]
    [SerializeField] private string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Cambia la gravedad solo para este rigidbody
                rb.useGravity = false;
                rb.AddForce(Vector3.up * nuevaGravedad, ForceMode.Acceleration);

                Debug.Log("Gravedad modificada para el jugador.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Restaura la gravedad normal
                rb.useGravity = true;

                Debug.Log("Gravedad restaurada.");
            }
        }
    }
}