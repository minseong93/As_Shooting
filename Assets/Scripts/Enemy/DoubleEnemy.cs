using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEnemy : Enemy, IDamageAble
{
    [SerializeField] private List<GameObject> attackPoint;
    protected override void DefineState()
    {
        states.Add("Idle", new DoubleIdleState(this));
        states.Add("Move", new DoubleMoveState(this));
        states.Add("Attack1", new DoubleAttackState(this));
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
        for(int i = 0; i < num; i++)
        {
            Bullet bullet_1 = GlobalPoolManager.GetBullet(enemyData.BulletType, attackPoint[0].transform.position, transform.rotation);
            Bullet bullet_2 = GlobalPoolManager.GetBullet(enemyData.BulletType, attackPoint[1].transform.position, transform.rotation);
            yield return Utilities.SetWait(0.1f);
        }
        SetState("Idle");
    }
}
public class DoubleIdleState : IEnemyState
{
    private Enemy enemy;
    public DoubleIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Idle(); }
    public void Exit() {}
}
public class DoubleMoveState : IEnemyState
{
    private Enemy enemy;
    public DoubleMoveState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Move(); }
    public void Exit() {}
}
public class DoubleAttackState : IEnemyState
{
    private Enemy enemy;
    private float attackCooldown = 2.0f;
    private float lastAttackTime;
    public DoubleAttackState(Enemy enemy)
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
