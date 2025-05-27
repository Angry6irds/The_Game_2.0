using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
     public GameObject cubePrefab;
    public float spawnInterval = 0.1f;

    // Área relativa al spawner
    public Vector2 localSpawnAreaMin = new Vector2(-1f, 0f);  // Min X,Y
    public Vector2 localSpawnAreaMax = new Vector2(1f, 1f);   // Max X,Y

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCube();
        }
    }

    void SpawnCube()
    {
         // Posición relativa al spawner
        float offsetX = Random.Range(localSpawnAreaMin.x, localSpawnAreaMax.x);
        float offsetY = Random.Range(localSpawnAreaMin.y, localSpawnAreaMax.y);
        Vector3 localOffset = new Vector3(offsetX, offsetY, 0f);

        // Sumar al position del spawner
        Vector3 spawnPos = transform.position + localOffset;

        GameObject newCube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);

        // Escala aleatoria (opcional)
        float size = Random.Range(0.1f, 0.3f);
        newCube.transform.localScale = Vector3.one * size;
    }
}
