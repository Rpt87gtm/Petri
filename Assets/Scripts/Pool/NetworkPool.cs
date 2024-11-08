using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO Test

public class NetworkPool<T> where T : NetworkBehaviour
{
    private T _prefab;
    private bool _autoExpand;
    private Transform _container;
    private List<T> _pool;

    public NetworkPool(T prefab, bool autoExpand = true, int count = 0, Transform container = null)
    {
        _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        _autoExpand = autoExpand;
        _container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>(count);
        for (int i = 0; i < count; i++)
        {
            CreateGameObject();
        }
    }

    private T CreateGameObject(bool isActiveByDefault = false)
    {
        var createdGameObject = UnityEngine.Object.Instantiate(_prefab, _container);
        createdGameObject.gameObject.SetActive(isActiveByDefault);
        NetworkServer.Spawn(createdGameObject.gameObject);
        _pool.Add(createdGameObject);
        return createdGameObject;
    }

    public bool HasFreeElement(out T element)
    {
        element = _pool.FirstOrDefault(item => !item.gameObject.activeInHierarchy);
        return element != null;
    }

    public T GetFreeElement(bool isActive = true)
    {
        if (HasFreeElement(out var element))
        {
            element.gameObject.SetActive(isActive);
            return element;
        }
        if (_autoExpand)
        {
            return CreateGameObject(isActive);
        }
        throw new PoolEmptyException($"No free elements in pool of type {typeof(T)}");
    }

    public List<T> GetFreeElements(int count, bool isActive = true)
    {
        List<T> freeElements = _pool.Where(item => !item.gameObject.activeInHierarchy).Take(count).ToList();
        int shortage = count - freeElements.Count;

        for (int i = 0; i < shortage && _autoExpand; i++)
        {
            freeElements.Add(CreateGameObject(isActive));
        }

        if (freeElements.Count < count)
            throw new PoolEmptyException($"Requested {count} elements, but pool only had {freeElements.Count}.");

        freeElements.ForEach(item => item.gameObject.SetActive(isActive));
        return freeElements;
    }

    public List<T> GetAllFreeElements(bool isActive = true)
    {
        var freeElements = _pool.Where(item => !item.gameObject.activeInHierarchy).ToList();
        if (isActive)
        {
            freeElements.ForEach(item => item.gameObject.SetActive(true));
        }
        return freeElements;
    }
     
    public int CountFreeElements()
    {
        return _pool.Count(item => !item.gameObject.activeInHierarchy);
    }
}

public class PoolEmptyException : Exception
{
    public PoolEmptyException(string message) : base(message) { }
}
