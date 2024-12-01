using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : Enemy, IDamageAble
{
    protected override void DefineState()
    {
        states.Add("Idle", new WaveIdleState(this));
        states.Add("Move", new WaveMoveState(this));
        states.Add("Attack1", new WaveAttackState(this));
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
            Bullet bullet = GlobalPoolManager.GetBullet(enemyData.BulletType, transform.position, transform.rotation);
            yield return Utilities.SetWait(0.1f);
        }
        SetState("Idle");
    }
}
public class WaveIdleState : IEnemyState
{
    private Enemy enemy;
    public WaveIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Idle(); }
    public void Exit() {}
}
public class WaveMoveState : IEnemyState
{
    private Enemy enemy;
    public WaveMoveState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter() {}
    public void Execute() { enemy.Move(); }
    public void Exit() {}
}
public class WaveAttackState : IEnemyState
{
    private Enemy enemy;
    private float attackCooldown = 2.0f;
    private float lastAttackTime;
    public WaveAttackState(Enemy enemy)
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
