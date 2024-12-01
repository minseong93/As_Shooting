using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Execute();
    void Exit();
}
public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    protected GameObject player;
    protected Dictionary<string, IEnemyState> states;


    public EnemyManager manager;
    private IEnemyState currentState;
    protected int currentHealth;
    private Vector3 setVec;

    protected virtual void Start()
    {
        manager = FindObjectOfType<EnemyManager>();
        manager?.RegisterEnemy(this);
        states = new Dictionary<string, IEnemyState>();
        DefineState();
        SetState("Idle");
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = enemyData.Health;
        manager?.RegisterEnemy(this);
    }
    public abstract void Damage();
    protected abstract void DefineState();
    protected virtual bool PlayerDistance(Transform transorm)
    {
        if (Vector3.Distance(player.transform.position, transform.position) > enemyData.AttackRich)
            return false;
        else
            return true;
    }
    protected virtual void SetState(string nowstate)
    {
        currentState?.Exit();

        if(states.ContainsKey(nowstate))
        {
            currentState = states[nowstate];
            currentState.Enter();
        }
    }
    public virtual void CustomUpdate()
    {
        currentState?.Execute();
    }
    public virtual void Idle()
    {
        if (!PlayerDistance(transform))
            SetState("Move");
        else
            SetState("Attack1");
    }
    public abstract void Attack();
    public virtual void Move()
    {
        if (player == null)
            SetState("Idle");
        else if (!PlayerDistance(transform))
            transform.Translate(-transform.up * enemyData.Speed * Time.deltaTime);
        else
            SetState("Attack1");
    }
    protected Vector3 SetVector3(float x, float y, float z)
    {
        setVec.x = x;
        setVec.y = y;
        setVec.z = z;
        return setVec;
    }
}
