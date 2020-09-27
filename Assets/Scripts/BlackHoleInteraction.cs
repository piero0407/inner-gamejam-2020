using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackHoleInteraction : MonoBehaviour
{
    [SerializeField] float addMass;
    [SerializeField] float destroyTimer;
    [SerializeField] float rateOfGrowth;
    [SerializeField] LayerMask layerMask;
    float destroyTime;
    bool isDestroying;

    Vector3 startingScale;

    private void Awake()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        if (isDestroying)
        {
            destroyTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startingScale, startingScale + Vector3.one * addMass, destroyTime / destroyTimer);
            if(destroyTime > destroyTimer)
            {
                destroyTime = 0f;
                isDestroying = false;

                transform.localScale = startingScale + Vector3.one * addMass;
                startingScale = transform.localScale;
            }
        }
        else
        {
            transform.localScale += Vector3.one * rateOfGrowth;
            startingScale = transform.localScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask.value) > 0)
        {
            Debug.Log("I collided with a blackhole. Shit.", other.gameObject);

            isDestroying = true;
            Destroy(other.gameObject);
        }
    }
}
