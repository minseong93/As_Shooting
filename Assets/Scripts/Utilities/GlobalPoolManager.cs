using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GlobalPoolManager : MonoBehaviour
{
    private static Dictionary<BulletPatternType, ObjectPooling<Bullet>> bulletPools;
    public static Dictionary<Tuple<EnemyType, Difficulty>, ObjectPooling<Enemy>> enemyPools;

    
    [System.Serializable]
    public class BulletPoolInfo
    {
        public BulletPatternType bulletType;
        public Bullet prefab;
        public int initialSize;
    }
    [System.Serializable]
    public class EnemyPoolInfo
    {
        public Enemy prefab;
        public int initialSize;
    }
    [SerializeField] private List<EnemyPoolInfo> enemyPoolInfos;
    [SerializeField] private List<BulletPoolInfo> bulletPoolInfos;
    [SerializeField] private Transform poolParent;

    private void Awake()
    {
        SetPools();
    }
    private void SetPools()
    {
        bulletPools = new Dictionary<BulletPatternType, ObjectPooling<Bullet>>();
        enemyPools = new Dictionary<Tuple<EnemyType, Difficulty>, ObjectPooling<Enemy>>();

        foreach (var poolInfo in bulletPoolInfos)
        {
            if (!bulletPools.ContainsKey(poolInfo.bulletType))
            {
                bulletPools[poolInfo.bulletType] = new ObjectPooling<Bullet>(poolInfo.prefab, poolInfo.initialSize, poolParent);
            }
        }

        foreach (var poolInfo in enemyPoolInfos)
        {
            if (!enemyPools.ContainsKey(Tuple.Create(poolInfo.prefab.enemyData.EnemyType, poolInfo.prefab.enemyData.DifficultyType)))
            {
                enemyPools[Tuple.Create(poolInfo.prefab.enemyData.EnemyType, poolInfo.prefab.enemyData.DifficultyType)] = new ObjectPooling<Enemy>(poolInfo.prefab, poolInfo.initialSize, poolParent);
            }
        }
    }

    public static Bullet GetBullet(BulletPatternType bulletType, Vector3 position, Quaternion rotation)
    {
        if (bulletPools.ContainsKey(bulletType))
        {
            return bulletPools[bulletType].GetObject(position, rotation);
        }
        else
        {
            Debug.LogError("없음");
            return null;
        }
    }
    public static Enemy GetEnemy(EnemyType enemyType, Difficulty difficulty, Vector3 position, Quaternion rotation)
    {
        if (enemyPools.ContainsKey(Tuple.Create(enemyType, difficulty)))
        {
            return enemyPools[Tuple.Create(enemyType, difficulty)].GetObject(position, rotation);
        }
        else
        {
            Debug.LogError("없음");
            return null;
        }
    }
    public static void ReturnEnemy(Enemy enemy, EnemyType enemyType, Difficulty difficulty)
    {
        if (enemyPools.ContainsKey(Tuple.Create(enemyType, difficulty)))
        {
            enemyPools[Tuple.Create(enemyType, difficulty)].ReturnObject(enemy);
        }
        else
        {
            //Debug.LogError("없음");
        }
    }
    public static void ReturnBullet(Bullet bullet, BulletPatternType bulletType)
    {
        if (bulletPools.ContainsKey(bulletType))
        {
            bulletPools[bulletType].ReturnObject(bullet);
        }
        else
        {
            Debug.LogError("없음");
        }
    }
}
