using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opciones : MonoBehaviour
{
    [Header("Referencias del UI")]
    public GameObject panelOpciones;
    public Slider sliderVolumen;
    public Slider sliderBrillo;
    public Button botonRegresar;
    public Button botonMenu;

    [Header("Configuracion de brillo")]
    public CanvasGroup canvasBrillo;

    private void Start()
    {
        // Cargar valores guardados
        float volumenGuardado = PlayerPrefs.GetFloat("Volumen", 1f);
        float brilloGuardado = PlayerPrefs.GetFloat("Brillo", 1f);

        sliderVolumen.value = volumenGuardado;
        sliderBrillo.value = brilloGuardado;

        // Aplicar configuraciones guardadas
        CambiarVolumen(volumenGuardado);
        CambiarBrillo(brilloGuardado);

        // Asignar eventos
        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        sliderBrillo.onValueChanged.AddListener(CambiarBrillo);
        botonRegresar.onClick.AddListener(CerrarOpciones);
        botonMenu.onClick.AddListener(IrAlMenu);
    }

    public void ActivarOpciones()
    {
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
    }

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("Volumen", valor);
    }

    public void CambiarBrillo(float valor)
    {
        if (canvasBrillo != null)
        {
            canvasBrillo.alpha = Mathf.Clamp01(valor);
            PlayerPrefs.SetFloat("Brillo", valor);
        }
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void TogglePanelOpciones()
    {
        panelOpciones.SetActive(!panelOpciones.activeSelf);
    }

}
