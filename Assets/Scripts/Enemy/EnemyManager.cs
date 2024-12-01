using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private List<StageData> stageDatas;
    [SerializeField] private int currentStageNum = 0;
    private List<float> usedPositions = new List<float>();
    private Presenter presenter;
    private bool checkNextStage = false;
    private bool checkStartStage = false;
    private float minDistance => 0.5f;
    private void Start()
    {
        presenter = GameObject.FindWithTag("Presenter").GetComponent<Presenter>();
    }

    private void LoadStage(int stageIndex)
    {
        if (stageIndex >= stageDatas.Count)
        {
            Debug.Log("모든 스테이지 클리어!");
            return;
        }

        usedPositions.Clear();
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        StageData currentStage = stageDatas[currentStageNum];
        if (currentStage.Enemies == null || currentStage.Enemies.Count == 0)
        {
            Debug.LogError("No enemies in the current stage!");
            return;
        }

        for (int i = 0; i < currentStage.EnemiesNum; i++)
        {
            float randomX = GenerateRandomPositionX();
            Vector3 spawnPosition = new Vector3(randomX, 6f, 0f);
            int randomEnemy = Random.Range(0, currentStage.Enemies.Count);
            Enemy selectedEnemy = currentStage.Enemies[randomEnemy];
            Enemy enemy = GlobalPoolManager.GetEnemy(selectedEnemy.enemyData.EnemyType, selectedEnemy.enemyData.DifficultyType, spawnPosition, Quaternion.Euler(0, 0, 180));
            checkNextStage = false;
        }
    }
    private float GenerateRandomPositionX()
    {
        const int maxrepeat = 100;
        int repeat = 0;
        float randomX = 0;
        while (repeat < maxrepeat)
        {
            randomX = Random.Range(-2.4f, 2.4f);
            bool isOver = true;

            foreach (float posX in usedPositions)
            {
                if (Mathf.Abs(posX - randomX) < minDistance)
                {
                    isOver = false;
                    break;
                }
            }
            if (isOver)
            {
                usedPositions.Add(randomX);
                return randomX;
            }

            repeat++;
        }
        return randomX;
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.CustomUpdate();
            }
            CheckStage();
        }
    }
    private void CheckStage()
    {
        if (enemies.Count == 0 && !checkNextStage)
        {
            presenter.SetStage();
            currentStageNum++;
            checkNextStage = true;
            LoadStage(currentStageNum);
        }
    }
    public void RegisterEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
}
