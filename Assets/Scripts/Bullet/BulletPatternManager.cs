using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternManager : MonoBehaviour
{
    private List<Bullet> bullets = new List<Bullet>();
    public void AddBullet(Bullet bullet)
    {
        if (!bullets.Contains(bullet))
        {
            bullets.Add(bullet);
        }
    }
    public void DeleteBullet(Bullet bullet)
    {
        if (bullets.Contains(bullet))
        {
            bullets.Remove(bullet);
        }
    }
    private void Update()
    {
        foreach (var bullet in bullets)
        {
            ExecutePattern(bullet);
        }
    }
    private void ExecutePattern(Bullet bullet)
    {
        
        if (bullet.PatternType == BulletPatternType.EnemyWave)
        {
            bullet.globalElapsedTime += Time.deltaTime;
            WavePattern(bullet);
        }
        else
            StraightPattern(bullet);
    }   

    private void StraightPattern(Bullet bullet)
    {
        bullet.transform.Translate(bullet.transform.up * bullet.speed * Time.deltaTime, Space.World);
    }

    private void WavePattern(Bullet bullet)
    {
        float waveOffset = Mathf.Sin(bullet.globalElapsedTime * bullet.wavePower);
        bullet.transform.Translate(bullet.transform.up * bullet.speed * Time.deltaTime, Space.World);
        bullet.transform.Translate(bullet.transform.right * waveOffset * Time.deltaTime * bullet.speed);

    }
}
