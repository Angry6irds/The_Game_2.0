using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiseñoMenu : MonoBehaviour
{
    public GameObject glitchPrefab;
    public GameObject circlePrefab;
    public int glitchCount = 6;
    public int circleCount = 2;
    public RectTransform canvasRect;
    void Start()
    {
        StartCoroutine(CrearGlitchBoxes(glitchCount));
        StartCoroutine(CrearCirculos(circleCount));
    }

    IEnumerator CrearGlitchBoxes(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject glitch = Instantiate(glitchPrefab, canvasRect);
            RectTransform rect = glitch.GetComponent<RectTransform>();
            float size = Random.Range(20f, 80f);
            rect.sizeDelta = new Vector2(size, size);
            rect.anchoredPosition = new Vector2(Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2),
                                                Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2));
            glitch.GetComponent<Animator>().SetFloat("Speed", Random.Range(0.5f, 1.5f));
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }
    }
    IEnumerator CrearCirculos(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject circle = Instantiate(circlePrefab, canvasRect);
            RectTransform rect = circle.GetComponent<RectTransform>();
            float size = Random.Range(400f, 500f);
            rect.sizeDelta = new Vector2(size, size);
            rect.anchoredPosition = new Vector2(Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2),
                                                Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2));
            circle.GetComponent<Animator>().SetFloat("Speed", Random.Range(0.3f, 1.2f));
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
