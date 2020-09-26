using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    //Inspector Properties
    [FoldoutGroup("References")]
    [SerializeField] new Rigidbody rigidbody;

    [TabGroup("TabGroup1", "Stats")]
    [HideLabel]
    [SerializeField] EntityStats stats;

    [TabGroup("TabGroup1","Collision")]
    [SerializeField] LayerMask damageLayers;

    //Logic Variables
    float currentLives;
    Vector2 m_Move;

    private void Awake()
    {
        currentLives = stats.MaxLives;
    }

    private void OnValidate()
    {
        if(stats != null && rigidbody != null)
            rigidbody.drag = stats.Drag;
    }

    private void FixedUpdate()
    {
        //Rotation: m_Move.x [-1...0...1]
        rigidbody.rotation *= Quaternion.Euler(0, 0, -m_Move.x * stats.AngularVelocity * Time.fixedDeltaTime);

        if (m_Move.y > 0f)
        {
            rigidbody.velocity += transform.up * m_Move.y * stats.Acceleration * Time.fixedDeltaTime; //Basic Movement
        }
        else
        {
            //TODO: "Dash" or Slingshot logic
        }

        //Limit rigidbody.velocity to MaxSpeed
        if (rigidbody.velocity.magnitude > stats.MaxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * stats.MaxSpeed;
        }
    }

    public void OnMove(InputValue value)
    {
        m_Move = value.Get<Vector2>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & damageLayers.value) > 0)
        {
            //TODO: Collision Logic
            print("I collided with something!!!");
        }
    }
}
