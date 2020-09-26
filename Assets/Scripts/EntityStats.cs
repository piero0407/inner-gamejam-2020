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

    public int MaxLives { get => maxLives; }
    public float AngularVelocity { get => angularVelocity; }
    public float Acceleration { get => acceleration; }
    public float MaxSpeed { get => maxSpeed; }
    public float Drag { get => drag; }
}
