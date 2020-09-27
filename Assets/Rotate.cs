using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float angularVelocity;

    void Update()
    {
        transform.Rotate(Vector3.forward * angularVelocity);
    }
}
