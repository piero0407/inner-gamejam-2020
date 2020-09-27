using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EntityController : MonoBehaviour
{
    //Inspector Properties
    [FoldoutGroup("References")]
    [SerializeField] new Rigidbody rigidbody;
    [FoldoutGroup("References")]
    [SerializeField] new MeshRenderer renderer;
    [FoldoutGroup("References")]
    [SerializeField] Slider slider;

    [TabGroup("TabGroup1", "Stats")]
    [HideLabel]
    [SerializeField] EntityStats stats;

    [TabGroup("TabGroup1","Collision")]
    [SerializeField] LayerMask damageLayers;
    [TabGroup("TabGroup1", "Collision")]
    [SerializeField] LayerMask killLayers;

    [TabGroup("TabGroup1", "Lifetime")]
    [SerializeField] GameObject spawnOnDead;
    [TabGroup("TabGroup1", "Lifetime")]
    [SerializeField] Material baseMaterial;
    [TabGroup("TabGroup1", "Lifetime")]
    [SerializeField] Material weakenedMaterial;

    //Logic Variables
    float currentLives;

    Vector2 m_Move;

    float slingshotTime;
    float slingshotCooldownTime;
    bool slingshotOnCooldown;
    bool onSlingshot;

    float invulnerableTime;
    bool invulnerable;

    [HideInInspector] public bool moveInputEnable = true;

    private void Awake()
    {
        currentLives = stats.MaxLives;
    }

    private void OnValidate()
    {
        if(stats != null && rigidbody != null)
            rigidbody.drag = stats.Drag;
    }

    private void Update()
    {
        if(invulnerable)
        {
            invulnerableTime += Time.deltaTime;
            if(invulnerableTime > stats.InvulnerableTimer)
            {
                invulnerableTime = 0f;
                invulnerable = false;
            }
        }

        if (slingshotOnCooldown)
        {
            slingshotCooldownTime -= Time.deltaTime;
            if (slingshotCooldownTime > 0f)
            {
                slingshotCooldownTime = 0f;
                slingshotOnCooldown = false;
            }
        }
        else
        {
            if (onSlingshot)
            {
                slingshotTime += Time.deltaTime;
                if (slingshotTime > stats.ChargeTimer)
                    slingshotTime = stats.ChargeTimer;
                slider.value = slingshotTime / stats.ChargeTimer;
            }
        }
    }

    private void FixedUpdate()
    {
        //Rotation: m_Move.x [-1...0...1]
        rigidbody.rotation *= Quaternion.Euler(0, 0, -m_Move.x * stats.AngularVelocity * Time.fixedDeltaTime);

        //Movement: m_Move.y [-1...0...1]
        //If m_Move.y > 0 then the player moves forward
        //If m_Move.y < 0 then the player charges the slingshot
        if (m_Move.y > 0f)
        {
            Vector3 velocity = transform.up * m_Move.y * stats.Acceleration * Time.fixedDeltaTime;

            //Limit velocity to MaxSpeed
            if (velocity.magnitude > stats.MaxSpeed)
                velocity = velocity.normalized * stats.MaxSpeed;

            rigidbody.velocity += velocity; 
        }
    }

    public void OnMove(InputValue value)
    {
        if (!moveInputEnable) return;

        m_Move = value.Get<Vector2>();

        if (m_Move.y == 0f && onSlingshot)
        {
            slider.gameObject.SetActive(false);
            invulnerable = true;

            rigidbody.AddForce(transform.up * stats.MaxPower * slider.value, ForceMode.Impulse);

            slingshotOnCooldown = true;
            slingshotCooldownTime = stats.ChargeCooldownTimer;

            slider.value = 0f;
            slingshotTime = 0f;
        }
        
        if(m_Move.y < 0f && !onSlingshot)
            slider.gameObject.SetActive(true);

        onSlingshot = m_Move.y < 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & damageLayers.value) > 0)
        {
            //TODO: Collision Logic
            print("I collided with something!!!");
            if(!invulnerable)
            {
                CheckLives();
            }
        }

        if (((1 << collision.gameObject.layer) & killLayers.value) > 0)
        {
            //TODO: Collision Logic
            print("I collided with walls!!!");
            if (!invulnerable)
            {
                CheckLives();
            }
            else
            {
                SpawnBlackHole();
            }
        }
    }

    private void SpawnBlackHole()
    {
        Instantiate(spawnOnDead, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    private void CheckLives()
    {
        currentLives--;
        if(currentLives == 1)
        {
            renderer.sharedMaterial = weakenedMaterial;
        }
        else if (currentLives == 0)
        {
            SpawnBlackHole();
        }
    }
}
