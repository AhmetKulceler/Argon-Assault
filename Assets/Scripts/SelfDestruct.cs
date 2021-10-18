using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float selfDestructDelay = 2f;

    void Start()
    {
        Destroy(gameObject, selfDestructDelay);
    }
}
