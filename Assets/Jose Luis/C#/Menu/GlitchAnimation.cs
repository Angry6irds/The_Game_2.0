using UnityEngine;

public class GlitchAnimation : MonoBehaviour
{
    private Vector3 posicionInicial;
    private float tiempoAleatorio;

    void Start()
    {
        posicionInicial = transform.position;
        tiempoAleatorio = Random.Range(0.05f, 0.2f);
        InvokeRepeating("GlitchMovimiento", tiempoAleatorio, tiempoAleatorio);
    }

    void GlitchMovimiento()
    {
        float offsetX = Random.Range(-0.3f, 0.3f);
        float offsetY = Random.Range(-0.3f, 0.3f);
        transform.position = posicionInicial + new Vector3(offsetX, offsetY, 0);
    }
}
