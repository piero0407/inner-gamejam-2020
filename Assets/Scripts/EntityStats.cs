using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[InlineEditor(Expanded = true)]
[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
public class EntityStats : ScriptableObject
{
    [BoxGroup("Base Stats")]
    [SerializeField] int maxLives = 2;

    [BoxGroup("Rotation Stats"), Range(100, 500)]
    [SerializeField] float angularVelocity;

    [BoxGroup("Movement Stats"), Range(1, 200)]
    [SerializeField] float acceleration;
    [BoxGroup("Movement Stats"), Range(1, 10)]
    [SerializeField] float maxSpeed;
    [BoxGroup("Movement Stats"), Range(1, 10)]
    [SerializeField] float drag = 5;

    [BoxGroup("Slingshot Stats"), Range(0.15f, 10)]
    [SerializeField] float chargeTimer;
    [BoxGroup("Slingshot Stats"), Range(0.15f, 10f)]
    [SerializeField] float chargeCooldownTimer;
    [BoxGroup("Slingshot Stats"), Range(1, 300)]
    [SerializeField] float maxPower;
    [BoxGroup("Invulnerable Stats"), Range(0.15f, 10)]
    [SerializeField] float invulnerableTimer;

    public int MaxLives { get => maxLives; }
    public float AngularVelocity { get => angularVelocity; }
    public float Acceleration { get => acceleration; }
    public float MaxSpeed { get => maxSpeed; }
    public float Drag { get => drag; }
    public float ChargeTimer { get => chargeTimer; }
    public float ChargeCooldownTimer { get => chargeCooldownTimer; }
    public float MaxPower { get => maxPower; }
    public float InvulnerableTimer { get => invulnerableTimer; }
}
