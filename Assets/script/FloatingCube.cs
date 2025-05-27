using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCube : MonoBehaviour
{
    public float speed = 1f;
    public float fadeDuration = 2f;

    private Material mat;
    private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
         // Instancia el material para evitar modificar el original
        mat = GetComponent<Renderer>().material;
        Color color = mat.color;
        color.a = 1f;
        mat.color = color;
        
    }

    // Update is called once per frame
    void Update()
    {
          // Mover hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Aumentar tiempo de vida
        lifetime += Time.deltaTime;

        // Calcular alpha
        float alpha = Mathf.Lerp(1f, 0f, lifetime / fadeDuration);
        Color color = mat.color;
        color.a = alpha;
        mat.color = color;

        // Destruir al final
        if (lifetime >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
