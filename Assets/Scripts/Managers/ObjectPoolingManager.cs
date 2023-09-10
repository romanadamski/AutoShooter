using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    [SerializeField]
    private List<Pool> pools = new List<Pool>();

    private void Start()
    {
        foreach (var pool in pools)
        {
            pool.Parent = CreateObjectsParent(pool.PoolableNameType);
            pool.Parent.name = pool.Parent.name.Replace("(Clone)", string.Empty);

            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                newObject.gameObject.SetActive(false);
                SetObjectName(newObject.gameObject);
                pool.PooledObjects.Enqueue(newObject);
                pool.ObjectCount++;
            }
        }
    }

    private void SetObjectName(GameObject poolableObject)
    {
        poolableObject.name = poolableObject.name.Replace("(Clone)", $"{poolableObject.GetInstanceID()}");
    }

    private Transform CreateObjectsParent(string parentName)
    {
        var parent = new GameObject(parentName + "Parent");
        parent.transform.SetParent(GameLauncher.Instance.GamePlane.transform);
        return parent.transform;
    }

    public BasePoolableController GetFromPool(string poolableType)
    {
        var pool = GetPoolByPoolableNameType(poolableType);
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {poolableType} type!");
            return null;
        }
        if (pool.PooledObjects.Count > 0)
        {
            var newObject = pool.PooledObjects.Dequeue();
            pool.ObjectsOutsidePool.Add(newObject);
            return newObject;
        }
        else
        {
            if (pool.CanGrow)
            {
                pool.ObjectCount++;
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                SetObjectName(newObject.gameObject);
                pool.ObjectsOutsidePool.Add(newObject);
                return newObject;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Returns all objects to their pools
    /// </summary>
    public void ReturnAllToPools()
    {
        foreach (var pool in pools)
        {
            pool.ReturnAllToPool();
        }
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByPoolableNameType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn);
    }

    private Pool GetPoolByPoolableNameType(string poolableType)
        => pools.FirstOrDefault(x => x.PoolableNameType.Equals(poolableType));
}
