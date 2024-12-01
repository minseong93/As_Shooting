using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> where T : Component
{
    private List<T> pooledObjects;
    private T prefab;
    private Transform parentTransform;

    public ObjectPooling(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parentTransform = parent;
        pooledObjects = new List<T>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab, parentTransform);
        newObj.gameObject.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    public T GetObject(Vector3 position, Quaternion rotation)
    {
        foreach (var obj in pooledObjects)
        {
            if (!obj.gameObject.activeSelf)
            {
                ActivateObject(obj, position, rotation);
                return obj;
            }
        }
        T newObj = CreateNewObject();
        ActivateObject(newObj, position, rotation);
        return newObj;
    }

    private void ActivateObject(T obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}