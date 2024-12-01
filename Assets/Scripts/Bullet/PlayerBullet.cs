using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Bullet")
        {
            IDamageAble damageAble = collision.GetComponent<IDamageAble>();
            damageAble?.Damage();
            bulletPatternManager.DeleteBullet(this);
            gameObject.SetActive(false);
        }
    }
}
