using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletPatternType
{
    PlayerStraight_Normal,
    PlayerStraight_Fast,
    PlayerStraight_Slow,
    EnemyStraight,
    EnemyWave,
}
public class Bullet : MonoBehaviour
{
    public float speed;
    public BulletPatternType PatternType;
    public float wavePower;
    public float globalElapsedTime = 0f;
    protected BulletPatternManager bulletPatternManager;
    private void OnEnable()
    {
        bulletPatternManager?.AddBullet(this);
    }
    private void Start()
    {
        bulletPatternManager = GameObject.FindGameObjectWithTag("BulletPatternManager").GetComponent<BulletPatternManager>();
        bulletPatternManager.AddBullet(this);
    }   
}
