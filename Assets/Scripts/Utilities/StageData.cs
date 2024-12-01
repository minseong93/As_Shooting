using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = int.MaxValue)]
public class StageData : ScriptableObject
{
    [SerializeField]private List<Enemy> enemies;
    public List<Enemy> Enemies { get { return enemies; } }
    [SerializeField] private int enemiesNum;
    public int EnemiesNum { get { return enemiesNum; } }
}
