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

    [Header("Primera Persona")]
    public bool enPrimeraPersona = false;
    private float rotacionX = 0f;
    private float rotacionY = 0f;
    public float sensibilidadMouse = 2f;
    public bool EnPrimeraPersona => enPrimeraPersona;
    private bool primerCambioAPrimera = true;

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            enPrimeraPersona = !enPrimeraPersona;
            primerCambioAPrimera = enPrimeraPersona;
        }

        if (enPrimeraPersona)
        {
            if (primerCambioAPrimera)
            {
                // Alineamos la cámara con el jugador al entrar en primera persona
                rotacionX = jugador.eulerAngles.y - 90f; // Porque tu personaje mira al eje X
                rotacionY = 0f;
                primerCambioAPrimera = false;
            }

            rotacionX += Input.GetAxis("Mouse X") * sensibilidadMouse;
            rotacionY -= Input.GetAxis("Mouse Y") * sensibilidadMouse;
            rotacionY = Mathf.Clamp(rotacionY, -80f, 80f);

            Quaternion rotacionFinal = Quaternion.Euler(rotacionY, rotacionX, 0);
            transform.position = Vector3.Lerp(transform.position, vistaPrimeraPersona.position, suavizado * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionFinal, suavizado * Time.deltaTime);
        }
        else
        {
            Vector3 destino = jugador.position + offsetTerceraPersona;
            transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
            Quaternion rotacion = Quaternion.Euler(rotacionTerceraPersona);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacion, suavizado * Time.deltaTime);
            // No tocar rotación del jugador aquí
        }
    }
}