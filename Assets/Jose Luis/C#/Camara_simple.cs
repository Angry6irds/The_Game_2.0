using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_simple : MonoBehaviour
{
    [Header("Referencias de cámara")]
    [SerializeField] private Transform puntoCamara; // Objeto que define la posición actual a seguir
    [SerializeField] private Transform vistaPrimeraPersona;
    [SerializeField] private Transform vistaTerceraPersona;

    [Header("Suavizado")]
    [SerializeField] private float velocidadSuavizado = 5f;
    private Transform objetivoActual;
    [Header("Rotación fija para tercera persona")]
    [SerializeField] private Vector3 rotacionFijaTerceraPersona = new Vector3(20, 0, 0);

    private void Start()
    {
        objetivoActual = vistaTerceraPersona;
        transform.rotation = Quaternion.Euler(rotacionFijaTerceraPersona);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (objetivoActual == vistaPrimeraPersona)
            {
                objetivoActual = vistaTerceraPersona;
            }
            else
            {
                objetivoActual = vistaPrimeraPersona;
            }
        }

        // Siempre sigue la posición del objetivo
        transform.position = Vector3.Lerp(transform.position, objetivoActual.position, velocidadSuavizado * Time.deltaTime);

        // Rotación dependiendo del modo
        if (objetivoActual == vistaPrimeraPersona)
        {
            // En primera persona, rota como el jugador
            transform.rotation = Quaternion.Lerp(transform.rotation, objetivoActual.rotation, velocidadSuavizado * Time.deltaTime);
        }
        else
        {
            // En tercera persona, rotación fija
            Quaternion rotacionDeseada = Quaternion.Euler(rotacionFijaTerceraPersona);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionDeseada, velocidadSuavizado * Time.deltaTime);
        }
    }
}
