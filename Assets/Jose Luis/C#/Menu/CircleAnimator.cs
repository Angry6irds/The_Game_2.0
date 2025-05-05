using UnityEngine;

public class CircleAnimator : MonoBehaviour
{
    private Vector3 initialScale;
    private float timeOffset;
    private Vector3 direction;

    void Start()
    {
        initialScale = transform.localScale;
        timeOffset = Random.Range(0f, 2f); // Para que no todos estén sincronizados
        direction = new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f),
            0f
        );
    }

    void Update()
    {
        float time = Time.time + timeOffset;

        // Movimiento suave oscilatorio
        transform.localPosition += direction * Mathf.Sin(time * 0.5f) * 0.01f;

        // Pulso suave
        float scale = Mathf.Lerp(0.9f, 1.1f, (Mathf.Sin(time) + 1f) / 2f);
        transform.localScale = initialScale * scale;

        // Suave fade in-out (opcional si usas un SpriteRenderer)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float alpha = Mathf.Lerp(0.3f, 0.8f, (Mathf.Sin(time * 0.8f) + 1f) / 2f);
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}
