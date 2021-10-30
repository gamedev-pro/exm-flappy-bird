using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IPooledObject
{
    void OnInstantiated();
    void OnEnabledFromPool();
    void OnDisabledFromPool();
}

[System.Serializable]
public class Pool<T> where T : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform poolRoot;
    [SerializeField] private T prefab;

    [SerializeField] private int initialObjectCount = 5;

    private List<T> objects;

    private bool IsInitialized => objects != null;

    public void Initialize()
    {
        objects = new List<T>(initialObjectCount);
        for (int i = 0; i < initialObjectCount; i++)
        {
            objects.Add(InstantiateObject());
        }
    }

    public T GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
        Assert.IsTrue(IsInitialized, "Please initialize pool before using it");
        T obj;
        if (objects.Count > 0)
        {
            obj = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
        }
        else
        {
            obj = InstantiateObject();
        }

        SetupObject(obj, position, rotation, parent);
        obj.gameObject.SetActive(true);
        obj.OnEnabledFromPool();

        return obj;
    }

    public void ReturnToPool(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(poolRoot);
            obj.OnDisabledFromPool();
            objects.Add(obj);
        }
    }

    private T InstantiateObject()
    {
        var obj = Object.Instantiate(prefab, poolRoot);
        obj.OnInstantiated();
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void SetupObject(T obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);
    }
}
