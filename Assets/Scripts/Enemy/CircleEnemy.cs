using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : Enemy, IDamageAble
{
    [SerializeField]private int bulletCount;
    private float minAngle => 60f;
    private float maxAngle => 120f;
    protected override void DefineState()
    {
        states.Add("Idle", new CircleIdleState(this));
        states.Add("Move", new CircleMoveState(this));
        states.Add("Attack1", new CircleAttackState(this));
    }
    public override void Attack()
    {
        float angle = Mathf.Atan2(player.transform.position.y - gameObject.transform.position.y, player.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        StartCoroutine(ContinuousAttack(enemyData.AttackCount));
        
    }
    public override void Damage()
    {
        currentHealth--;
        if (currentHealth == 0)
        {
            StopAllCoroutines();
            GlobalPoolManager.ReturnEnemy(this, enemyData.EnemyType, enemyData.DifficultyType);
            manager?.UnregisterEnemy(this);
            gameObject.SetActive(false);
        }
    }
    public IEnumerator ContinuousAttack(int num)
    {
        float angle = (maxAngle - minAngle) / (bulletCount - 1);
        float rotationOffset = 90f;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                Bullet bullet = GlobalPoolManager.GetBullet(enemyData.BulletType, transform.position, transform.rotation);
                float bulletAngle = minAngle + (angle * j) + rotationOffset;
                bullet.transform.eulerAngles = SetVector3(0, 0, bulletAngle);
            }
            yield return Utilities.SetWait(0.1f);
        }
        SetState("Idle");
    }
}
public class CircleIdleState : IEnemyState
{
    private Enemy enemy;
    public CircleIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Idle(); }
    public void Exit() {}
}
public class CircleMoveState : IEnemyState
{
    private Enemy enemy;
    public CircleMoveState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Move(); }
    public void Exit() {}
}
public class CircleAttackState : IEnemyState
{
    private Enemy enemy;
    private float attackCooldown = 2.0f;
    private float lastAttackTime;
    public CircleAttackState(Enemy enemy)
    {
        this.enemy = enemy;
        lastAttackTime = -attackCooldown;
    }
    public void Enter() {}
    public void Execute() 
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            enemy.Attack();
            lastAttackTime = Time.time;
        }
    }
    public void Exit() {}
}
