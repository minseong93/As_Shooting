using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, IDamageAble
{    
     private Vector3 setVec;
    [SerializeField] protected PlayerData playerData;

    protected virtual void Start()
    {
        PlayerEvent playerEvent = GetComponent<PlayerEvent>();
        
        if (playerEvent == null)
            Debug.LogError("EventIsNull");
        else
        {
            playerEvent.OnInputMove += OnMoveEvent;
            playerEvent.OnInputMouse += OnMouseEvent;
            playerEvent.OnClickMouse += OnMouseClickEvent;
        }
    }
    public abstract void Damage();
    protected abstract void OnMoveEvent(Vector3 velocityVec);
    protected abstract void OnMouseEvent();
    protected abstract void OnMouseClickEvent();

    protected Vector3 SetVector3(float x, float y, float z)
    {
        setVec.x = x;
        setVec.y = y;
        setVec.z = z;
        return setVec;
    }
}
