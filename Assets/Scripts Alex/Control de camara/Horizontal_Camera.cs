using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_Camera : MonoBehaviour
{
    public Transform target;
    public float fixedY;
    public float fixedZ;

    void LateUpdate()
    {
        float newY = Mathf.Max(target.position.y, fixedY);
        transform.position = new Vector3(target.position.x, newY, fixedZ);
    }
}