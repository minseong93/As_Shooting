using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]private int health;
    public int Health { get { return health; } }
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }
    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }
    [SerializeField]
    private BulletPatternType bulletType;
    public BulletPatternType BulletType { get { return bulletType; } }
}
