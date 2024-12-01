using UnityEngine;
public enum EnemyType
{
    Straight,
    Wave,
    Circle,
    Double
}
public enum Difficulty
{
    VeryEasy,
    Easy,
    Normal,
    Hard,
    VeryHard
}
[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private int health;
    public int Health { get { return health; } }
    [SerializeField]
    private float attackPower;
    public float AttackPower { get { return attackPower; } }
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }
    [SerializeField]
    private float attackRich;
    public float AttackRich { get { return attackRich; } }
    [SerializeField]
    private int attackCount;
    public int AttackCount { get { return attackCount; } }
    [SerializeField]
    private BulletPatternType bulletType;
    public BulletPatternType BulletType { get { return bulletType; } }
    [SerializeField]
    private EnemyType enemyType;
    public EnemyType EnemyType { get { return enemyType; } }
    [SerializeField]
    private Difficulty difficultyType;
    public Difficulty DifficultyType { get { return difficultyType; } }
}
