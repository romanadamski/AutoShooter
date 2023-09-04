using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool
{
    public int StartPoolCount;
    public BasePoolableController PoolObjectPrefab;
    public bool CanGrow;
    public Queue<GameObject> PooledObjects = new Queue<GameObject>();

    [HideInInspector]
    public int ObjectCount;

    /// <summary>
    /// Type choosen in prefab from Poolable type dropdown
    /// </summary>
    public string PoolableNameType => PoolObjectPrefab.GetComponent<BasePoolableController>().PoolableType;

    /// <summary>
    /// Type choosen by attached Poolable component
    /// </summary>
    public Type PoolableComponentType => PoolObjectPrefab.GetComponent<BasePoolableController>().GetType();

    [HideInInspector]
    public List<GameObject> ObjectsOutsidePool = new List<GameObject>();

    public void ReturnAllToPool()
    {
        foreach (var poolObject in ObjectsOutsidePool.ToList())
        {
            ReturnToPool(poolObject);
        }
        ObjectsOutsidePool.Clear();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        if (!PooledObjects.Contains(objectToReturn.gameObject))
        {
            objectToReturn.gameObject.SetActive(false);
            PooledObjects.Enqueue(objectToReturn.gameObject);
        }
    }
}