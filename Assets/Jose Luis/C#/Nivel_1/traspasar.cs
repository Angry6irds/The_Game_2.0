using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Tutorial UI")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private TextMeshProUGUI textoTutorial;
    [TextArea(3, 5)]
    [SerializeField] private string mensajeTutorial;
    [SerializeField] private List<GameObject> imagenesTutorial;
    [SerializeField] private float tiempoEntreImagenes = 1.2f;

    private bool tutorialActivo = false;
    private bool tutorialListo = false;
    private GameObject jugador;

    void Update()
    {
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        // Si el tutorial ya terminó, puedes presionar E para cerrarlo
        if (tutorialActivo && tutorialListo && Input.GetKeyDown(KeyCode.E))
        {
            CerrarTutorial();
        }

        // Activar habilidad si ya tienes el cristal y no estás en tutorial
        if (Input.GetKeyDown(ActivarCristal) && !enTemporizador && objetoRecogido && !tutorialActivo)
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
                jugador = col.gameObject;

                // Bloquear movimiento del jugador
                Player_Standar mov = jugador.GetComponent<Player_Standar>();
                if (mov != null)
                    mov.puedeMoverse = false;

                objetoRecogido = true;

                // Ocultar el objeto visualmente
                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                Debug.Log("Cristal de traspaso recogido. Iniciando tutorial.");
                StartCoroutine(MostrarTutorial());
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger el cristal.");
    }

    // Tutorial con texto gradual + imágenes con fade in
    private IEnumerator MostrarTutorial()
    {
        panelTutorial.SetActive(true);

        if (textoTutorial != null)
            yield return StartCoroutine(EscribirTextoGradualmente(mensajeTutorial));

        foreach (GameObject img in imagenesTutorial)
        {
            if (img != null)
            {
                CanvasGroup cg = img.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    img.SetActive(true);
                    yield return StartCoroutine(FadeInCanvasGroup(cg, 0.5f));
                }
                else
                {
                    img.SetActive(true);
                }

                yield return new WaitForSeconds(tiempoEntreImagenes);
            }
        }

        tutorialActivo = true;
        tutorialListo = true;
    }

    // Cierra el tutorial y reactiva movimiento
    private void CerrarTutorial()
    {
        panelTutorial.SetActive(false);

        if (jugador != null)
        {
            Player_Standar p = jugador.GetComponent<Player_Standar>();
            if (p != null)
                p.puedeMoverse = true;
        }

        tutorialActivo = false;
        tutorialListo = false;
    }

    // Animación de texto tipo máquina de escribir
    private IEnumerator EscribirTextoGradualmente(string texto)
    {
        textoTutorial.text = "";
        foreach (char letra in texto)
        {
            textoTutorial.text += letra;
            yield return new WaitForSeconds(0.03f);
        }
    }

    // Animación de aparición suave para cada imagen
    private IEnumerator FadeInCanvasGroup(CanvasGroup cg, float duracion)
    {
        float tiempo = 0f;
        cg.alpha = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(tiempo / duracion);
            yield return null;
        }

        cg.alpha = 1f;
    }

    IEnumerator TemporizadorTraspaso()
    {
        enTemporizador = true; // Bloquear para evitar múltiples activaciones

        // Buscar el objeto con el tag "Objeto_Traspasable"
        GameObject[] objetosTraspasables = GameObject.FindGameObjectsWithTag(ObjetoTraspasable);

        /* Para Ale: objetosTraspasables es un arreglo de GameObjects. Este arreglo contiene
         todos los objetos en la escena que tienen el tag "Objeto_Traspasable".
         Lo que hace foreach: es recorrer todos los elementos de un arreglo uno por uno.
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

            // El segundo foreach es para desactivar el trigger
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