using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traspasar : MonoBehaviour
{
    [SerializeField] private CapsuleCollider capsuleCollider;
    void Start()
    {
        
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.F))
        {
         
            capsuleCollider.isTrigger = !capsuleCollider.isTrigger;
            Debug.Log("isTrigger ahora está en: " + capsuleCollider.isTrigger);
        }
    }
}
