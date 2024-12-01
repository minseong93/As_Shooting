using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    protected override void OnMoveEvent(Vector3 velocityVec)
    {
        transform.Translate(velocityVec * playerData.Speed * Time.deltaTime, Space.World);
        transform.position = SetVector3(Mathf.Clamp(transform.position.x, -2.4f, 2.4f), Mathf.Clamp(transform.position.y, -4.7f, 4.7f), 0);
    }
    protected override void OnMouseEvent()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - gameObject.transform.position.y, mouse.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    protected override void OnMouseClickEvent()
    {
        Bullet bullet = GlobalPoolManager.GetBullet(playerData.BulletType, transform.position, transform.rotation);
    }
    public override void Damage()
    {
        currenthealth--;
        if (currenthealth == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
