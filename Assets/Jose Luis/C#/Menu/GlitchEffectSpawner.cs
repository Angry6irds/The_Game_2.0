using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GlitchEffectSpawner : MonoBehaviour
{
    public int glitchCount = 6;
    public int circleCount = 2;
    public Canvas targetCanvas;

    void Start()
    {
        CreateGlitchBoxes(glitchCount);
        CreateCircles(circleCount);
    }

    void CreateGlitchBoxes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject glitch = Instantiate(Resources.Load<GameObject>("GlitchBox"), targetCanvas.transform);
            RectTransform rect = glitch.GetComponent<RectTransform>();
            float size = Random.Range(20f, 100f);
            rect.sizeDelta = new Vector2(size, size);
            PositionRandom(rect);
            AnimateGlitch(rect);
        }
    }

    void CreateCircles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject circle = Instantiate(Resources.Load<GameObject>("Circle"), targetCanvas.transform);
            RectTransform rect = circle.GetComponent<RectTransform>();
            float size = Random.Range(400f, 900f);
            rect.sizeDelta = new Vector2(size, size);
            PositionRandom(rect);
            AnimateCircle(rect);
        }
    }

    void PositionRandom(RectTransform rect)
    {
        rect.anchoredPosition = new Vector2(
            Random.Range(-Screen.width / 2f, Screen.width / 2f),
            Random.Range(-Screen.height / 2f, Screen.height / 2f)
        );
    }

    void AnimateGlitch(RectTransform rect)
    {
        Vector2 original = rect.anchoredPosition;
        Vector2 offset = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        float duration = Random.Range(6f, 10f);
        float delay = Random.Range(0f, 3f);
        Image img = rect.GetComponent<Image>();

        img.DOFade(0f, 0f); // start invisible

        Sequence seq = DOTween.Sequence().SetLoops(-1);
        seq.AppendInterval(delay);
        seq.Append(img.DOFade(0.5f, duration * 0.25f));
        seq.Join(rect.DOAnchorPos(original + offset, duration * 0.25f));
        seq.Append(img.DOFade(0.7f, duration * 0.25f));
        seq.Join(rect.DOAnchorPos(original - offset, duration * 0.25f));
        seq.Append(img.DOFade(0.5f, duration * 0.25f));
        seq.Join(rect.DOAnchorPos(original + offset, duration * 0.25f));
        seq.Append(img.DOFade(0f, duration * 0.25f));
        seq.Join(rect.DOAnchorPos(original, duration * 0.25f));
    }

    void AnimateCircle(RectTransform rect)
    {
        Vector2 original = rect.anchoredPosition;
        Vector2 offset = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        float duration = Random.Range(6f, 10f);
        float delay = Random.Range(0f, 3f);
        Image img = rect.GetComponent<Image>();

        rect.localScale = Vector3.one * 0.8f;
        img.DOFade(0f, 0f); // start invisible

        Sequence seq = DOTween.Sequence().SetLoops(-1);
        seq.AppendInterval(delay);
        seq.Append(img.DOFade(0.8f, duration * 0.5f));
        seq.Join(rect.DOAnchorPos(original + offset, duration * 0.5f));
        seq.Join(rect.DOScale(1f, duration * 0.5f));
        seq.Append(img.DOFade(0f, duration * 0.5f));
        seq.Join(rect.DOAnchorPos(original, duration * 0.5f));
        seq.Join(rect.DOScale(0.8f, duration * 0.5f));
    }
}