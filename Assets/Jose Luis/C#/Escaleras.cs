using UnityEngine;

public class Escaleras : MonoBehaviour
{
    [Tooltip("Velocidad al subir la escalera")]
    public float nuevaVelocidad;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Standar jugador = collision.gameObject.GetComponent<Player_Standar>();
            if (jugador != null)
            {
                jugador.SetVelocidadTemporal(nuevaVelocidad);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Standar jugador = collision.gameObject.GetComponent<Player_Standar>();
            if (jugador != null)
            {
                jugador.RestaurarVelocidad();
            }
        }
    }
}
