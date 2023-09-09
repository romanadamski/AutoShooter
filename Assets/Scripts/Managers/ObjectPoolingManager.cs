using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    [SerializeField]
    private List<Pool> pools = new List<Pool>();
    public List<Pool> Pools => pools;

    private void Start()
    {
        foreach (var pool in pools)
        {
            pool.Parent = CreateObjectsParent(pool.PoolableNameType);
            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab.gameObject, pool.Parent);
                newObject.gameObject.SetActive(false);
                SetPoolableObjectName(newObject);
                pool.PooledObjects.Enqueue(newObject);
                pool.ObjectCount++;
            }
        }
    }

    private void SetPoolableObjectName(GameObject poolableObject)
    {
        poolableObject.name = poolableObject.name.Replace("(Clone)", $"{poolableObject.GetInstanceID()}");
    }

    private Transform CreateObjectsParent(string parentName)
        => Instantiate(new GameObject(parentName + "Parent").transform, GameLauncher.Instance.GamePlane.transform);

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
            var newObject = pool.PooledObjects.Dequeue().GetComponent<BasePoolableController>();
            pool.ObjectsOutsidePool.Add(newObject);
            return newObject;
        }
        else
        {
            if (pool.CanGrow)
            {
                pool.ObjectCount++;
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                SetPoolableObjectName(newObject.gameObject);
                pool.ObjectsOutsidePool.Add(newObject.GetComponent<BasePoolableController>());
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
