using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup titleGroup;
    public TextMeshProUGUI titleText;
    public CanvasGroup menuGroup;
    public Button[] menuButtons;

    [Header("Animation Settings")]
    public float titleFadeDuration = 2f;
    public float menuFadeDuration = 3f;
    public float buttonHoverScale = 1.1f;
    public float hoverDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la UI invisible
        titleGroup.alpha = 0;
        menuGroup.alpha = 0;

        // Animación de entrada para el título
        titleGroup.DOFade(1, titleFadeDuration).SetEase(Ease.InOutSine);

        // Mostrar menú después del título
        DOVirtual.DelayedCall(titleFadeDuration, () => {
            menuGroup.DOFade(1, menuFadeDuration).SetEase(Ease.InOutSine);
        });

        // Asignar efectos a botones
        foreach (var button in menuButtons)
        {
            var rect = button.GetComponent<RectTransform>();
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            AddHoverEvent(trigger, EventTriggerType.PointerEnter, () => {
                rect.DOScale(buttonHoverScale, hoverDuration).SetEase(Ease.OutBack);
            });

            AddHoverEvent(trigger, EventTriggerType.PointerExit, () => {
                rect.DOScale(1f, hoverDuration).SetEase(Ease.InOutQuad);
            });
        }
    }

    void AddHoverEvent(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction action)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((_) => action());
        trigger.triggers.Add(entry);
    }

    // Métodos llamados desde los botones
    public void StartNewGame()
    {
        Debug.Log("Iniciar nueva partida...");
        // Cargar escena o lógica de nuevo juego
    }

    public void LoadGame()
    {
        Debug.Log("Cargar partida...");
    }
    public void OpenOptions()
    {
        Debug.Log("Abrir opciones...");
    }
    public void ShowCredits()
    {
        Debug.Log("Mostrar créditos...");
    }
    public void ExitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
