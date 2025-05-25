using UnityEngine;

public class CamaraMixta : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform jugador;
    [SerializeField] private Transform vistaPrimeraPersona;

    [Header("Configuración tercera persona")]
    [SerializeField] private Vector3 offsetTerceraPersona = new Vector3(0, 2, -4);
    [SerializeField] private Vector3 rotacionTerceraPersona = new Vector3(10, 0, 0);

    [Header("Transición")]
    [SerializeField] private float suavizado = 5f;

    private bool enPrimeraPersona = false;

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            enPrimeraPersona = !enPrimeraPersona;
        }

        if (enPrimeraPersona)
        {
            // Primera persona: posición y rotación exacta
            transform.position = Vector3.Lerp(transform.position, vistaPrimeraPersona.position, suavizado * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, vistaPrimeraPersona.rotation, suavizado * Time.deltaTime);
        }
        else
        {
            // Tercera persona: sigue al jugador desde un offset global (NO hijo)
            Vector3 destino = jugador.position + offsetTerceraPersona;
            transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);

            Quaternion rotacion = Quaternion.Euler(rotacionTerceraPersona);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacion, suavizado * Time.deltaTime);
        }
    }
}