using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Bullet")
        {
            GlobalPoolManager.ReturnBullet(this, PatternType);
            bulletPatternManager.DeleteBullet(this);
            gameObject.SetActive(false);
        }
    }
}